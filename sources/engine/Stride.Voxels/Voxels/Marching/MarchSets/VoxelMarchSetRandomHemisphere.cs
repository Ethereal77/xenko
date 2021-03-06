// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2019 Sean Boettger <sean@whypenguins.com>
// See the LICENSE.md file in the project root for full license information.

using Stride.Core;
using Stride.Shaders;

namespace Stride.Rendering.Voxels
{
    [DataContract(DefaultMemberMode = DataMemberMode.Default)]
    [Display("Random Hemisphere")]
    public class VoxelMarchSetRandomHemisphere : IVoxelMarchSet
    {
        public IVoxelMarchMethod Marcher { set; get; } = new VoxelMarchConePerMipmap();

        public int Count = 6;

        public bool AnimateNoise = false;

        float time = 0f;


        public VoxelMarchSetRandomHemisphere() { }

        public VoxelMarchSetRandomHemisphere(IVoxelMarchMethod marcher)
        {
            Marcher = marcher;
        }


        public ShaderSource GetMarchingShader(int attrID)
        {
            var mixin = new ShaderMixinSource();
            mixin.Mixins.Add(new ShaderClassSource("VoxelMarchSetRandomHemisphere"));
            mixin.AddComposition("Marcher", Marcher.GetMarchingShader(attrID));

            return mixin;
        }

        ValueParameterKey<int> CountKey;
        ValueParameterKey<float> TimeKey;

        public void UpdateMarchingLayout(string compositionName)
        {
            Marcher.UpdateMarchingLayout("Marcher." + compositionName);
            CountKey = VoxelMarchSetRandomHemisphereKeys.marchCount.ComposeWith(compositionName);
            TimeKey = VoxelMarchSetRandomHemisphereKeys.time.ComposeWith(compositionName);
        }

        public void ApplyMarchingParameters(ParameterCollection parameters)
        {
            time += Count * 3.73f;
            if (time > 4000f)
                time = 0f;

            Marcher.ApplyMarchingParameters(parameters);
            parameters.Set(CountKey, Count);
            parameters.Set(TimeKey, AnimateNoise ? time : 0f);
        }
    }
}
