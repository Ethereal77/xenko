// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2019 Sean Boettger <sean@whypenguins.com>
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core;
using Stride.Shaders;

namespace Stride.Rendering.Voxels
{
    [DataContract(DefaultMemberMode = DataMemberMode.Default)]
    [Display("Hemisphere (12)")]
    public class VoxelMarchSetHemisphere12 : VoxelMarchSetBase, IVoxelMarchSet
    {
        public VoxelMarchSetHemisphere12() { }

        public VoxelMarchSetHemisphere12(IVoxelMarchMethod marcher)
        {
            Marcher = marcher;
        }


        public ShaderSource GetMarchingShader(int attrID)
        {
            var mixin = new ShaderMixinSource();
            mixin.Mixins.Add(new ShaderClassSource("VoxelMarchSetHemisphere12"));
            mixin.AddComposition("Marcher", Marcher.GetMarchingShader(attrID));

            return mixin;
        }

        public override void UpdateMarchingLayout(string compositionName)
        {
            base.UpdateMarchingLayout(compositionName);
            OffsetKey = VoxelMarchSetHemisphere12Keys.offset.ComposeWith(compositionName);
        }
    }
}
