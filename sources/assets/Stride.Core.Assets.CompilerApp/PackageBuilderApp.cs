// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

using Mono.Options;

using Stride.Core.Yaml;
using Stride.Core.Diagnostics;
using Stride.Core.BuildEngine;
using Stride.Core.Assets.Diagnostics;
using Stride.Core.Assets.CompilerApp.Tasks;
using Stride.Core.VisualStudio;
using Stride.Assets.Models;
using Stride.Assets.SpriteFont;
using Stride.Rendering.Materials;
using Stride.Rendering.ProceduralModels;
using Stride.Particles;
using Stride.SpriteStudio.Offline;

namespace Stride.Core.Assets.CompilerApp
{
    class PackageBuilderApp : IPackageBuilderApp
    {
        private static Stopwatch clock;

        private LogListener globalLoggerOnGlobalMessageLogged;

        private PackageBuilder builder;

        public bool IsSlave { get; private set; }

        public int Run(string[] args)
        {
            // This is used by ExecServer to retrieve the logs directly without using the console redirect (which is not working well
            // in a multi-domain scenario)
            var redirectLogToAppDomainAction = AppDomain.CurrentDomain.GetData("AppDomainLogToAction") as Action<string, ConsoleColor>;

            clock = Stopwatch.StartNew();

            // TODO: This is hardcoded. Check how to make this dynamic instead.
            RuntimeHelpers.RunModuleConstructor(typeof(IProceduralModel).Module.ModuleHandle);
            RuntimeHelpers.RunModuleConstructor(typeof(MaterialKeys).Module.ModuleHandle);
            RuntimeHelpers.RunModuleConstructor(typeof(SpriteFontAsset).Module.ModuleHandle);
            RuntimeHelpers.RunModuleConstructor(typeof(ModelAsset).Module.ModuleHandle);
            RuntimeHelpers.RunModuleConstructor(typeof(SpriteStudioAnimationAsset).Module.ModuleHandle);
            RuntimeHelpers.RunModuleConstructor(typeof(ParticleSystem).Module.ModuleHandle);
            //var project = new Package();
            //project.Save("test.sdpkg");

            //Thread.Sleep(10000);
            //var spriteFontAsset = StaticFontAsset.New();
            //Content.Save("test.sdfnt", spriteFontAsset);
            //project.Refresh();

            //args = new string[] { "test.sdpkg", "-o:app_data", "-b:tmp", "-t:1" };

            var exeName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            var showHelp = false;
            var packMode = false;
            var buildEngineLogger = GlobalLogger.GetLogger("BuildEngine");
            var options = new PackageBuilderOptions(new ForwardingLoggerResult(buildEngineLogger));

            var p = new OptionSet
            {
                "Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)",
                "Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)",
                "Stride Build Tool - Version: "
                +
                String.Format(
                    "{0}.{1}.{2}",
                    typeof(Program).Assembly.GetName().Version.Major,
                    typeof(Program).Assembly.GetName().Version.Minor,
                    typeof(Program).Assembly.GetName().Version.Build) + string.Empty,
                string.Format("Usage: {0} inputPackageFile [options]* -b buildPath", exeName),
                string.Empty,
                "=== Options ===",
                string.Empty,
                { "h|help", "Show this message and exit", v => showHelp = v != null },
                { "v|verbose", "Show more verbose progress logs", v => options.Verbose = v != null },
                { "d|debug", "Show debug logs (imply verbose)", v => options.Debug = v != null },
                { "log", "Enable file logging", v => options.EnableFileLogging = v != null },
                { "disable-auto-compile", "Disable auto-compile of projects", v => options.DisableAutoCompileProjects = v != null},
                { "project-configuration=", "Project configuration", v => options.ProjectConfiguration = v },
                { "platform=", "Platform name", v => options.Platform = (PlatformType)Enum.Parse(typeof(PlatformType), v) },
                { "solution-file=", "Solution File Name", v => options.SolutionFile = v },
                { "package-id=", "Package Id from the solution file", v => options.PackageId = Guid.Parse(v) },
                { "package-file=", "Input Package File Name", v => options.PackageFile = v },
                { "msbuild-uptodatecheck-filebase=", "BuildUpToDate File base for MSBuild; it will create one .inputs and one .outputs files", v => options.MSBuildUpToDateCheckFileBase = v },
                { "o|output-path=", "Output path", v => options.OutputDirectory = v },
                { "b|build-path=", "Build path", v => options.BuildDirectory = v },
                { "log-file=", "Log build in a custom file.", v =>
                {
                    options.EnableFileLogging = v != null;
                    options.CustomLogFileName = v;
                } },
                { "log-pipe=", "Log pipe.", v =>
                {
                    if (!string.IsNullOrEmpty(v))
                        options.LogPipeNames.Add(v);
                } },
                { "monitor-pipe=", "Monitor pipe.", v =>
                {
                    if (!string.IsNullOrEmpty(v))
                        options.MonitorPipeNames.Add(v);
                } },
                { "slave=", "Slave pipe", v => options.SlavePipe = v }, // Benlitz: I don't think this should be documented
                { "server=", "This Compiler is launched as a server", v => { } },
                { "pack", "Special mode to copy assets and resources in a folder for NuGet packaging", v => packMode = true },
                { "t|threads=", "Number of threads to create. Default value is the number of hardware threads available.", v => options.ThreadCount = int.Parse(v) },
                { "test=", "Run a test session.", v => options.TestName = v },
                { "property:", "Properties. Format is name1=value1;name2=value2", v =>
                {
                    if (!string.IsNullOrEmpty(v))
                    {
                        foreach (var nameValue in v.Split(new [] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var equalIndex = nameValue.IndexOf('=');
                            if (equalIndex == -1)
                                throw new OptionException("Expect name1=value1;name2=value2 format.", "property");

                            options.Properties.Add(nameValue.Substring(0, equalIndex), nameValue.Substring(equalIndex + 1));
                        }
                    }
                }
                },
                { "compile-property:", "Compile properties. Format is name1=value1;name2=value2", v =>
                {
                    if (!string.IsNullOrEmpty(v))
                    {
                        if (options.ExtraCompileProperties is null)
                            options.ExtraCompileProperties = new Dictionary<string, string>();

                        foreach (var nameValue in v.Split(new [] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var equalIndex = nameValue.IndexOf('=');
                            if (equalIndex == -1)
                                throw new OptionException("Expect name1=value1;name2=value2 format.", "property");

                            options.ExtraCompileProperties.Add(nameValue.Substring(0, equalIndex), nameValue.Substring(equalIndex + 1));
                        }
                    }
                }
                },
                {
                    "reattach-debugger=", "Reattach to a Visual Studio debugger", v =>
                    {
                        if (!string.IsNullOrEmpty(v) && int.TryParse(v, out var debuggerProcessId))
                        {
                            if (!Debugger.IsAttached)
                            {
                                using (var debugger = VisualStudioDebugger.GetByProcess(debuggerProcessId))
                                {
                                    debugger?.Attach();
                                }
                            }
                        }
                    }
                },
            };

            TextWriterLogListener fileLogListener = null;

            BuildResultCode exitCode;

            RemoteLogForwarder assetLogger = null;

            try
            {
                var unexpectedArgs = p.Parse(args);

                // Set remote logger
                assetLogger = new RemoteLogForwarder(options.Logger, options.LogPipeNames);
                GlobalLogger.GlobalMessageLogged += assetLogger;

                // Activate proper log level
                buildEngineLogger.ActivateLog(options.LoggerType);

                // Output logs to the console with colored messages
                if (options.SlavePipe is null && !options.LogPipeNames.Any())
                {
                    if (redirectLogToAppDomainAction != null)
                    {
                        globalLoggerOnGlobalMessageLogged = new LogListenerRedirectToAction(redirectLogToAppDomainAction);
                    }
                    else
                    {
                        globalLoggerOnGlobalMessageLogged = new ConsoleLogListener { LogMode = ConsoleLogMode.Always };
                    }
                    globalLoggerOnGlobalMessageLogged.TextFormatter = FormatLog;
                    GlobalLogger.GlobalMessageLogged += globalLoggerOnGlobalMessageLogged;
                }

                if (unexpectedArgs.Any())
                {
                    throw new OptionException("Unexpected arguments [{0}]".ToFormat(string.Join(", ", unexpectedArgs)), "args");
                }
                try
                {
                    options.ValidateOptions();
                }
                catch (ArgumentException ex)
                {
                    throw new OptionException(ex.Message, ex.ParamName);
                }

                if (showHelp)
                {
                    p.WriteOptionDescriptions(Console.Out);
                    return (int)BuildResultCode.Successful;
                }
                else if (packMode)
                {
                    PackageSessionPublicHelper.FindAndSetMSBuildVersion();

                    var csprojFile = options.PackageFile;
                    var intermediatePackagePath = options.BuildDirectory;
                    var generatedItems = new List<(string SourcePath, string PackagePath)>();
                    var logger = new LoggerResult();
                    if (!PackAssetsHelper.Run(logger, csprojFile, intermediatePackagePath, generatedItems))
                    {
                        foreach (var message in logger.Messages)
                        {
                            Console.WriteLine(message);
                        }
                        return (int)BuildResultCode.BuildError;
                    }
                    foreach (var generatedItem in generatedItems)
                    {
                        Console.WriteLine($"{generatedItem.SourcePath}|{generatedItem.PackagePath}");
                    }
                    return (int)BuildResultCode.Successful;
                }

                // Also write logs from master process into a file
                if (options.SlavePipe == null)
                {
                    if (options.EnableFileLogging)
                    {
                        string logFileName = options.CustomLogFileName;
                        if (string.IsNullOrEmpty(logFileName))
                        {
                            string inputName = Path.GetFileNameWithoutExtension(options.PackageFile);
                            logFileName = "Logs/Build-" + inputName + "-" + DateTime.Now.ToString("yy-MM-dd-HH-mm") + ".txt";
                        }

                        string dirName = Path.GetDirectoryName(logFileName);
                        if (dirName != null)
                            Directory.CreateDirectory(dirName);

                        fileLogListener = new TextWriterLogListener(new FileStream(logFileName, FileMode.Create)) { TextFormatter = FormatLog };
                        GlobalLogger.GlobalMessageLogged += fileLogListener;
                    }

                    options.Logger.Info("BuildEngine arguments: " + string.Join(" ", args));
                    options.Logger.Info("Starting builder.");
                }
                else
                {
                    IsSlave = true;
                }

                if (!string.IsNullOrEmpty(options.TestName))
                {
                    var test = new TestSession();
                    test.RunTest(options.TestName, options.Logger);
                    exitCode = BuildResultCode.Successful;
                }
                else
                {
                    builder = new PackageBuilder(options);
                    if (!IsSlave && redirectLogToAppDomainAction == null)
                    {
                        Console.CancelKeyPress += OnConsoleOnCancelKeyPress;
                    }
                    exitCode = builder.Build();
                }
            }
            catch (OptionException e)
            {
                options.Logger.Error($"Command option '{e.OptionName}': {e.Message}");
                exitCode = BuildResultCode.CommandLineError;
            }
            catch (Exception e)
            {
                options.Logger.Error($"Unhandled exception", e);
                exitCode = BuildResultCode.BuildError;
            }
            finally
            {
                // Flush and close remote logger
                if (assetLogger != null)
                {
                    GlobalLogger.GlobalMessageLogged -= assetLogger;
                    assetLogger.Dispose();
                }

                if (fileLogListener != null)
                {
                    GlobalLogger.GlobalMessageLogged -= fileLogListener;
                    fileLogListener.LogWriter.Close();
                }

                // Output logs to the console with colored messages
                if (globalLoggerOnGlobalMessageLogged != null)
                {
                    GlobalLogger.GlobalMessageLogged -= globalLoggerOnGlobalMessageLogged;
                }
                if (builder != null && !IsSlave && redirectLogToAppDomainAction == null)
                {
                    Console.CancelKeyPress -= OnConsoleOnCancelKeyPress;
                }

                // Reset cache hold by YamlSerializer
                YamlSerializer.Default.ResetCache();
            }
            return (int)exitCode;
        }

        private void OnConsoleOnCancelKeyPress(object _, ConsoleCancelEventArgs e)
        {
            e.Cancel = builder.Cancel();
        }

        private static string FormatLog(ILogMessage message)
        {
            //$filename($row,$column): $error_type $error_code: $error_message
            //C:\Code\Stride\sources\assets\Stride.Core.Assets.CompilerApp\PackageBuilder.cs(89,13,89,70): warning CS1717: Assignment made to same variable; did you mean to assign something else?

            var builder = new StringBuilder();

            // Location
            if (message is AssetLogMessage assetLogMessage)
                builder.Append($"{assetLogMessage.File}({assetLogMessage.Line + 1},{assetLogMessage.Character + 1}): ");
            // Message type
            builder.Append(message.Type.ToString().ToLowerInvariant()).Append(" ");
            builder.Append((clock.ElapsedMilliseconds * 0.001).ToString("0.000"));
            builder.Append("s: ");
            builder.Append($"[{message.Module ?? "AssetCompiler"}] ");
            builder.Append(message.Text);
            var exceptionInfo = message.ExceptionInfo;
            if (exceptionInfo != null)
            {
                builder.Append(". Exception: ");
                builder.Append(exceptionInfo);
            }
            return builder.ToString();
        }
    }
}
