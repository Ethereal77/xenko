// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core;

namespace Stride.Graphics
{
    /// <summary>
    /// The graphics platform.
    /// </summary>
    [DataContract("GraphicsPlatform")]
    public enum GraphicsPlatform
    {
        /// <summary>
        /// The Null Renderer / Shader.
        /// </summary>
        Null,

        /// <summary>
        /// Direct3D 11 Renderer / HLSL Shader.
        /// </summary>
        Direct3D11,

        /// <summary>
        /// Direct3D 12 Renderer / HLSL Shader.
        /// </summary>
        Direct3D12
    }
}
