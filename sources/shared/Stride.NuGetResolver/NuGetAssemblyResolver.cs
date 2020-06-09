// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;

using NuGet.Common;
using NuGet.Configuration;
using NuGet.Frameworks;
using NuGet.Versioning;

namespace Stride.Core.Assets
{
    class NuGetAssemblyResolver
    {
        public const string DevSource = @"%LocalAppData%\Stride\NugetDev";

        static bool assembliesResolved;
        static object assembliesLock = new object();
        static List<string> assemblies;

        internal static void DisableAssemblyResolve()
        {
            assembliesResolved = true;
        }

        [ModuleInitializer(-100000)]
        internal static void __Initialize__()
        {
            // Only perform this for entry assembly
            if (!(Assembly.GetEntryAssembly() is null ||                        // .NET FW: null during module .ctor
                Assembly.GetEntryAssembly() == Assembly.GetCallingAssembly()))  // .NET Core: Check against calling assembly
                return;

            // Make sure our NuGet local store is added to NuGet config
            var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string strideFolder = null;
            while (folder != null)
            {
                if (File.Exists(Path.Combine(folder, @"build\Stride.sln")))
                {
                    strideFolder = folder;
                    var settings = NuGet.Configuration.Settings.LoadDefaultSettings(null);

                    Directory.CreateDirectory(Environment.ExpandEnvironmentVariables(DevSource));
                    CheckPackageSource(settings, "Stride Dev", NuGet.Configuration.Settings.ApplyEnvironmentTransform(DevSource));

                    settings.SaveToDisk();
                    break;
                }
                folder = Path.GetDirectoryName(folder);
            }

            // NOTE: We perform NuGet restore inside the assembly resolver rather than top level module ctor (otherwise it freezes)
            AppDomain.CurrentDomain.AssemblyResolve += (sender, eventArgs) =>
            {
                if (!assembliesResolved)
                {
                    lock (assembliesLock)
                    {
                        // NOTE: Using NuGet will try to recursively resolve NuGet.*.resources.dll, so set assembliesResolved right away so that it bypasses everything
                        assembliesResolved = true;

                        var logger = new Logger();
                        var previousSynchronizationContext = SynchronizationContext.Current;
                        try
                        {
                            // Since we execute restore synchronously, we don't want any surprise concerning synchronization context (i.e. Avalonia one doesn't work with this)
                            SynchronizationContext.SetSynchronizationContext(null);

                            // Determine current TFM
                            var framework = Assembly
                                .GetEntryAssembly()?
                                .GetCustomAttribute<TargetFrameworkAttribute>()?
                                .FrameworkName ?? ".NETFramework,Version=v4.7.2";
                            var nugetFramework = NuGetFramework.ParseFrameworkName(framework, DefaultFrameworkNameProvider.Instance);

                            // Only allow this specific version
                            var versionRange = new VersionRange(new NuGetVersion(StrideVersion.NuGetVersion), true, new NuGetVersion(StrideVersion.NuGetVersion), true);
                            var (request, result) = RestoreHelper.Restore(logger, nugetFramework, "win", Assembly.GetExecutingAssembly().GetName().Name, versionRange).Result;
                            if (!result.Success)
                            {
                                throw new InvalidOperationException($"Could not restore NuGet packages");
                            }

                            assemblies = RestoreHelper.ListAssemblies(result.LockFile);
                        }
                        catch (Exception e)
                        {
                            var logFile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
                            var logText = $@"Error restoring NuGet packages!

==== Exception details ====

{e}

==== Log ====

{string.Join(Environment.NewLine, logger.Logs.Select(x => $"[{x.Level}] {x.Message}"))}
";
                            File.WriteAllText(logFile, logText);
#if STRIDE_NUGET_RESOLVER_UX
                            // Write log to file
                            System.Windows.Forms.MessageBox.Show($"{e.Message}{Environment.NewLine}{Environment.NewLine}Please see details in {logFile} (which will be automatically opened)", "Error restoring NuGet packages");
                            Process.Start(logFile);
#else
                            // Display log in console
                            Console.WriteLine(logText);
#endif
                            Environment.Exit(1);
                        }
                        finally
                        {
                            SynchronizationContext.SetSynchronizationContext(previousSynchronizationContext);
                        }
                    }
                }

                if (assemblies != null)
                {
                    var aname = new AssemblyName(eventArgs.Name);
                    if (aname.Name.StartsWith("Microsoft.Build") && aname.Name != "Microsoft.Build.Locator")
                        return null;
                    var assemblyPath = assemblies.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x) == aname.Name);
                    if (assemblyPath != null)
                    {
                        return Assembly.LoadFrom(assemblyPath);
                    }
                }
                return null;
            };
        }

        private static void RemoveSources(ISettings settings, string prefixName)
        {
            var packageSources = settings.GetSection("packageSources");
            if (packageSources != null)
            {
                foreach (var packageSource in packageSources.Items.OfType<SourceItem>().ToList())
                {
                    var path = packageSource.GetValueAsPath();

                    if (packageSource.Key.StartsWith(prefixName))
                    {
                        // Remove entry from packageSources
                        settings.Remove("packageSources", packageSource);
                    }
                }
            }
        }

        private static void CheckPackageSource(ISettings settings, string name, string url)
        {
            settings.AddOrUpdate("packageSources", new SourceItem(name, url));
        }

        public class Logger : ILogger
        {
            private object logLock = new object();
            public List<(LogLevel Level, string Message)> Logs { get; } = new List<(LogLevel, string)>();

            public void LogDebug(string data)
            {
                Log(LogLevel.Debug, data);
            }

            public void LogVerbose(string data)
            {
                Log(LogLevel.Verbose, data);
            }

            public void LogInformation(string data)
            {
                Log(LogLevel.Information, data);
            }

            public void LogMinimal(string data)
            {
                Log(LogLevel.Minimal, data);
            }

            public void LogWarning(string data)
            {
                Log(LogLevel.Warning, data);
            }

            public void LogError(string data)
            {
                Log(LogLevel.Error, data);
            }

            public void LogInformationSummary(string data)
            {
                Log(LogLevel.Information, data);
            }

            public void LogErrorSummary(string data)
            {
                Log(LogLevel.Error, data);
            }

            public void Log(LogLevel level, string data)
            {
                lock (logLock)
                {
                    Debug.WriteLine($"[{level}] {data}");
                    Logs.Add((level, data));
                }
            }

            public Task LogAsync(LogLevel level, string data)
            {
                Log(level, data);
                return Task.CompletedTask;
            }

            public void Log(ILogMessage message)
            {
                Log(message.Level, message.Message);
            }

            public Task LogAsync(ILogMessage message)
            {
                Log(message);
                return Task.CompletedTask;
            }
        }
    }
}
