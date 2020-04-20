// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Stride.Rendering
{
    /// <summary>
    /// Defines keys associated with mesh used for compiling assets.
    /// </summary>
    public sealed class MaterialAssetKeys
    {
        /// <summary>
        /// When compiling effect with an EffectLibraryAsset (xkfxlib), set it to true to allow permutation based on the 
        /// parameters of all materials.
        /// </summary>
        /// <userdoc>
        /// Use the material parameters to generate effects
        /// </userdoc>
        public static readonly ValueParameterKey<bool> UseParameters = ParameterKeys.NewValue<bool>();

        /// <summary>
        /// Allow material compilation without mesh.
        /// </summary>
        /// <userdoc>
        /// Generate a shader even if the materials aren't attached to a mesh
        /// </userdoc>
        public static readonly ValueParameterKey<bool> GenerateShader = ParameterKeys.NewValue<bool>();
    }
}
