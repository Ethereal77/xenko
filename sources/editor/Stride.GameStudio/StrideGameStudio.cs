// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#pragma warning disable 436 // The type 'type' in 'assembly' conflicts with the imported type 'type2' in 'assembly' (due to StrideVersion being duplicated)

using Stride.Core.Annotations;
using Stride.Metrics;

namespace Stride.GameStudio
{
    public static class StrideGameStudio
    {
        [NotNull]
        public static string CopyrightText1 => "© 2018 Stride contributors";

        [NotNull]
        public static string CopyrightText2 => "© 2011-2018 Silicon Studio Corp.";

        [NotNull]
        public static string EditorName => $"Stride Game Studio {EditorVersion}";

        [NotNull]
        public static string EditorVersion => StrideVersion.NuGetVersion;

        [NotNull]
        public static string EditorVersionWithMetadata => StrideVersion.NuGetVersion + StrideVersion.BuildMetadata;

        public static string EditorVersionMajor => new System.Version(StrideVersion.PublicVersion).ToString(2);

        [NotNull]
        public static string AnswersUrl => "http://answers.stride3d.net/";

        [NotNull]
        public static string DocumentationUrl => $"https://doc.stride3d.net/{EditorVersionMajor}/";

        [NotNull]
        public static string ForumsUrl => "https://forums.stride3d.net/";

        [NotNull]
        public static string ReportIssueUrl => "https://github.com/stride3d/stride/issues/";

        public static MetricsClient MetricsClient;
    }
}
