// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.ComponentModel;

using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Graphics;

namespace Stride.Assets.Textures
{
    [DataContract("NormalMapTextureType")]
    [Display("Normal Map")]
    public class NormapMapTextureType : ITextureType
    {
        public bool IsSRgb(ColorSpace colorSpaceReference) => false;

        /// <summary>
        /// Indicating whether the Y-component of normals should be inverted, to compensate for a flipped tangent-space.
        /// </summary>
        /// <userdoc>
        /// Indicates that a positive Y-component (green) faces up in tangent space. This options depends on your normal maps generation tools.
        /// </userdoc>
        [DataMember(10)]
        [DefaultValue(true)]
        public bool InvertY { get; set; } = true;

        bool ITextureType.ColorKeyEnabled => false;

        Color ITextureType.ColorKeyColor => new Color();

        AlphaFormat ITextureType.Alpha => AlphaFormat.None;

        bool ITextureType.PremultiplyAlpha => false;

        TextureHint ITextureType.Hint => TextureHint.NormalMap;
    }
}
