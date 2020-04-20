// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Linq;

namespace Stride.Rendering.Images
{
    /// <summary>
    /// Keys used by <see cref="AmbientOcclusionBlur"/> and AmbientOcclusionBlurEffect xkfx
    /// </summary>
    internal static class AmbientOcclusionBlurKeys
    {
        public static readonly PermutationParameterKey<int> Count = ParameterKeys.NewPermutation<int>(9);

        public static readonly PermutationParameterKey<bool> VerticalBlur = ParameterKeys.NewPermutation<bool>();

        public static readonly PermutationParameterKey<float> BlurScale = ParameterKeys.NewPermutation<float>(2f);

        public static readonly PermutationParameterKey<float> EdgeSharpness = ParameterKeys.NewPermutation<float>(4f);
    }
}
