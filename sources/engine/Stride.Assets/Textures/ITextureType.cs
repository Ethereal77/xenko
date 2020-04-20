// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core.Mathematics;
using Stride.Graphics;

namespace Stride.Assets.Textures
{
    public interface ITextureType
    {
        bool IsSRgb(ColorSpace colorSpaceReference);

        bool ColorKeyEnabled { get; }

        Color ColorKeyColor { get; }

        AlphaFormat Alpha { get; }

        bool PremultiplyAlpha { get; }

        TextureHint Hint { get; }
    }
}
