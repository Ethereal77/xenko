// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.ComponentModel;

using Stride.Core.Assets;
using Stride.Core;
using Stride.Animations;
using Stride.Assets;

namespace Stride.SpriteStudio.Offline
{
    [DataContract("SpriteStudioAnimationAsset")] // Name of the Asset serialized in YAML
    [AssetContentType(typeof(AnimationClip))]
    [AssetDescription(FileExtension)] // A description used to display in the asset editor
    [Display("SpriteStudio animation")]
    [AssetFormatVersion(StrideConfig.PackageName, CurrentVersion, "2.0.0.0")]
    public class SpriteStudioAnimationAsset : AssetWithSource
    {
        public const string FileExtension = ".sdss4a";

        private const string CurrentVersion = "2.0.0.0";

        [DataMember(1)]
        [DefaultValue(AnimationRepeatMode.LoopInfinite)]
        public AnimationRepeatMode RepeatMode { get; set; } = AnimationRepeatMode.LoopInfinite;

        [DataMember(2)]
        [Display(Browsable = false)]
        [DefaultValue("")]
        public string AnimationName { get; set; } = "";
    }
}
