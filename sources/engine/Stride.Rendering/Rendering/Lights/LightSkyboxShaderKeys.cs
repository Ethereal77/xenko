// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Shaders;

namespace Stride.Rendering.Lights
{
    public partial class LightSkyboxShaderKeys
    {
        public static readonly PermutationParameterKey<ShaderSource> LightDiffuseColor = ParameterKeys.NewPermutation<ShaderSource>();
        public static readonly PermutationParameterKey<ShaderSource> LightSpecularColor = ParameterKeys.NewPermutation<ShaderSource>();
    }
}
