// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Stride.GitVersioning
{
    public class GenerateVersionFile : Task
    {
        /// <summary>
        ///   Gets or sets the version file.
        /// </summary>
        /// <value>The version file.</value>
        [Required]
        public ITaskItem VersionFile { get; set; }

        /// <summary>
        ///   The output file for the version information.
        /// </summary>
        [Required]
        public ITaskItem GeneratedVersionFile { get; set; }

        /// <summary>
        ///   Gets or sets the root directory.
        /// </summary>
        [Required]
        public ITaskItem RootDirectory { get; set; }

        [Output]
        public string NuGetVersion { get; set; }

        public string NuGetVersionSuffixOverride { get; set; }

        public string SpecialVersion { get; set; }

        public bool RevisionGitHeight { get; set; }

        public bool SpecialVersionGitCommit { get; set; }

        public override bool Execute()
        {
            if (RootDirectory is null || !Directory.Exists(RootDirectory.ItemSpec))
            {
                Log.LogError("PackageFile is not set or doesn't exist.");
                return false;
            }

            if (VersionFile is null || !File.Exists(Path.Combine(RootDirectory.ItemSpec, VersionFile.ItemSpec)))
            {
                Log.LogError("VersionFile is not set or doesn't exist.");
                return false;
            }

            if (GeneratedVersionFile is null)
            {
                Log.LogError("OutputVersionFile is not set.");
                return false;
            }

            var currentAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            var mainPlatformDirectory = Path.GetFileName(Path.GetDirectoryName(currentAssemblyLocation));

            // TODO: Right now we patch the VersionFile, but ideally we should make a copy and make the build system use it
            var versionFileData = File.ReadAllText(Path.Combine(RootDirectory.ItemSpec, VersionFile.ItemSpec));

            var publicVersionMatch = Regex.Match(versionFileData, "PublicVersion = \"(.*)\";");
            var versionSuffixMatch = Regex.Match(versionFileData, "NuGetVersionSuffix = \"(.*)\";");
            var publicVersion = publicVersionMatch.Success ? publicVersionMatch.Groups[1].Value : "0.0.0.0";
            var versionSuffix = versionSuffixMatch.Success ? versionSuffixMatch.Groups[1].Value : string.Empty;

            if (NuGetVersionSuffixOverride != null)
                versionSuffix = NuGetVersionSuffixOverride;

            // Patch NuGetVersion
            if (SpecialVersion != null)
                versionSuffix += SpecialVersion;

            EnsureLibGit2UnmanagedInPath(mainPlatformDirectory);

            // Compute Git Height using Nerdbank.GitVersioning
            // For now we assume top level package directory is git folder
            try
            {
                var rootDirectory = RootDirectory.ItemSpec;

                var repo = LibGit2Sharp.Repository.IsValid(rootDirectory) ? new LibGit2Sharp.Repository(rootDirectory) : null;
                if (repo is null)
                {
                    Log.LogError("Could not open Git repository.");
                    return false;
                }

                // Patch AssemblyInformationalVersion
                var headCommitSha = repo.Head.Commits.FirstOrDefault()?.Sha;

                if (RevisionGitHeight)
                {
                    if (!Version.TryParse(publicVersion, out var publicVersionParsed))
                        throw new InvalidOperationException($"Could not decode version {publicVersion}.");

                    // Compute version based on Git info
                    var height = Nerdbank.GitVersioning.GitExtensions.GetVersionHeight(repo, VersionFile.ItemSpec);
                    publicVersionParsed = new Version(publicVersionParsed.Major, publicVersionParsed.Minor, publicVersionParsed.Build, height);
                    publicVersion = publicVersionParsed.ToString();

                    versionFileData = Regex.Replace(versionFileData, "PublicVersion = \"(.*)\";", $"PublicVersion = \"{publicVersion}\";");
                }

                // Replace NuGetVersionSuffix
                versionFileData = Regex.Replace(versionFileData, "NuGetVersionSuffix = \"(.*)\";", $"NuGetVersionSuffix = \"{versionSuffix}\";");

                // Always include git commit (even if not part of NuGetVersionSuffix)
                if (SpecialVersionGitCommit && headCommitSha != null)
                {
                    // Replace build metadata
                    versionFileData = Regex.Replace(versionFileData, "BuildMetadata = (.*);", $"BuildMetadata = \"+g{headCommitSha.Substring(0, 8)}\";");
                }

                // Write back new file
                File.WriteAllText(Path.Combine(RootDirectory.ItemSpec, GeneratedVersionFile.ItemSpec), versionFileData);

                NuGetVersion = publicVersion + versionSuffix;

                return true;
            }
            catch (Exception ex)
            {
                NuGetVersion = publicVersion + versionSuffix;
                Log.LogWarning($"Could not determine version using Git history: {ex}", ex);
                return false;
            }
        }

        private static void EnsureLibGit2UnmanagedInPath(string mainPlatformDirectory)
        {
            // On .NET Framework (on Windows), we find native binaries by adding them to our PATH.
            var libgit2Directory = Nerdbank.GitVersioning.GitExtensions.FindLibGit2NativeBinaries(mainPlatformDirectory);
            if (libgit2Directory != null)
            {
                string pathEnvVar = Environment.GetEnvironmentVariable("PATH");
                string[] searchPaths = pathEnvVar.Split(Path.PathSeparator);
                if (!searchPaths.Contains(libgit2Directory, StringComparer.OrdinalIgnoreCase))
                {
                    pathEnvVar += Path.PathSeparator + libgit2Directory;
                    Environment.SetEnvironmentVariable("PATH", pathEnvVar);
                }
            }
        }
    }
}
