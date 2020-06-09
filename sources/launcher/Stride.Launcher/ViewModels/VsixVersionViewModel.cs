// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Stride.Core.Extensions;
using Stride.Core.VisualStudio;
using Stride.Core.Packages;
using Stride.Core.Presentation.Commands;
using Stride.Core.Presentation.Services;
using Stride.LauncherApp.Resources;
using Stride.LauncherApp.Services;

namespace Stride.LauncherApp.ViewModels
{
    internal sealed class VsixVersionViewModel : PackageVersionViewModel
    {
        private readonly string packageId;

        /// <inheritdoc/>
        public override string Name => Strings.VisualStudioPlugin;

        /// <inheritdoc/>
        public override string FullName => Name;

        /// <summary>
        ///   Gets a value indicating whether the latest version of the VSIX package is installed.
        /// </summary>
        /// <remarks>
        ///   This property is updated by <see cref="UpdateFromStore"/> and requires the latest NuGet package to be in the local store.
        /// </remarks>
        public bool IsLatestVersionInstalled { get { return isLatestVersionInstalled; } private set { SetValue(ref isLatestVersionInstalled, value); } }
        private bool isLatestVersionInstalled;

        /// <summary>
        ///   Gets the current status of the VSIX package.
        /// </summary>
        public string Status { get { return status; } private set { SetValue(ref status, value); } }
        private string status;

        /// <summary>
        /// Gets a command that will download the latest version of the VSIX and install it on all compatible versions of Visual Studio.
        /// </summary>
        public ICommandBase ExecuteActionCommand { get; }

        /// <inheritdoc/>
        protected override string InstallErrorMessage => Strings.ErrorInstallingVSIX;

        /// <inheritdoc/>
        protected override string UninstallErrorMessage => Strings.ErrorUninstallingVSIX;


        internal VsixVersionViewModel(LauncherViewModel launcher, NugetStore store, string packageId)
            : base(launcher, store, null)
        {
            this.packageId = packageId;
            status = FormatStatus(Strings.ReportChecking);
            ExecuteActionCommand = new AnonymousCommand(ServiceProvider, ExecuteAction) { IsEnabled = false };
        }


        public async Task UpdateFromStore()
        {
            Dispatcher.Invoke(() => Status = FormatStatus(Strings.ReportChecking));
            await UpdateVersionsFromStore();
            Dispatcher.Invoke(UpdateStatus);
        }

        /// <inheritdoc/>
        protected override void UpdateStatus()
        {
            base.UpdateStatus();
            var newStatus = Strings.VSIXVerbReinstall;
            if (CanBeDownloaded)
            {
                newStatus = LocalPackage == null ? Strings.VSIXVerbInstall : Strings.VSIXVerbUpdate;
                IsLatestVersionInstalled = false;
            }
            ExecuteActionCommand.IsEnabled = true;
            Status = FormatStatus(newStatus);
        }

        private string FormatStatus(string status)
        {
            return $"{packageId.Split('.')[0]}: {status}";
        }

        /// <inheritdoc/>
        protected override void UpdateInstallStatus()
        {
            switch (CurrentProgressAction)
            {
                case ProgressAction.Download:
                    CurrentProcessStatus = string.Format(Strings.ReportDownloadingVSIX, CurrentProgress);
                    break;

                case ProgressAction.Install:
                    CurrentProcessStatus = string.Format(Strings.ReportInstallingVSIX, CurrentProgress);
                    break;

                case ProgressAction.Delete:
                    CurrentProcessStatus = string.Format(Strings.ReportDeletingVersion, FullName, CurrentProgress);
                    break;
            }
        }

        /// <inheritdoc/>
        protected override async Task UpdateVersionsFromStore()
        {
            LocalPackage = await Launcher.RunLockTask(() => Store.GetLocalPackages(packageId).OrderByDescending(p => p.Version).FirstOrDefault());
            ServerPackage = await Launcher.RunLockTask(() => Store.FindSourcePackagesById(packageId, CancellationToken.None).Result.OrderByDescending(p => p.Version).FirstOrDefault());
        }

        private void ExecuteAction()
        {
            Task.Run(async () =>
            {
                await Download(false);

                IsProcessing = true;
                string checkingStatus = Strings.ReportChecking;
                try
                {
                    CurrentProcessStatus = checkingStatus;
                    IsProcessing = false;
                    await ServiceProvider.Get<IDialogService>().MessageBox(Strings.VSIXInstallSucessful, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception e)
                {
                    CurrentProcessStatus = checkingStatus;
                    IsProcessing = false;
                    var message = $"{Strings.ErrorInstallingVSIX}{e.FormatSummary(true)}";
                    await ServiceProvider.Get<IDialogService>().MessageBox(message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                UpdateStatus();
            });
        }
    }
}
