// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core.Mathematics;

namespace Stride.Rendering.Colors
{
    /// <summary>
    /// Defines the interface for describing the color of a light.
    /// </summary>
    public interface IColorProvider
    {
        /// <summary>
        /// Computes the color of the light (sRgb space).
        /// </summary>
        /// <returns>Color3.</returns>
        Color3 ComputeColor();
    }
}
