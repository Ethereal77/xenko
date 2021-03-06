// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Linq;

using Stride.Core.Assets.Editor.ViewModel;
using Stride.Core.IO;

namespace Stride.Core.Assets.Editor.Quantum.NodePresenters.Commands
{
    class AssetInitialDirectoryProvider : IInitialDirectoryProvider
    {
        private readonly SessionViewModel session;

        public AssetInitialDirectoryProvider(SessionViewModel session)
        {
            this.session = session;
        }

        public UDirectory GetInitialDirectory(UDirectory currentPath)
        {
            if (session != null && session.ActiveAssetView.SelectedAssets.Count == 1 && session.ActiveAssetView.SelectedAssetsPackage != null && currentPath != null)
            {
                var asset = session.ActiveAssetView.SelectedAssets.First();
                var projectPath = session.ActiveAssetView.SelectedAssetsPackage.PackagePath;
                if (projectPath != null)
                {
                    var assetFullPath = UPath.Combine(projectPath.GetFullDirectory(), new UFile(asset.Url));

                    if (string.IsNullOrWhiteSpace(currentPath))
                    {
                        return assetFullPath.GetFullDirectory();
                    }
                    var defaultPath = UPath.Combine(assetFullPath.GetFullDirectory(), currentPath);
                    return defaultPath.GetFullDirectory();
                }
            }
            return currentPath;
        }
    }
}
