// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2019 Sean Boettger <sean@whypenguins.com>
// See the LICENSE.md file in the project root for full license information.

using Stride.Core;
using Stride.Shaders;

namespace Stride.Rendering.Voxels
{
    [DataContract(DefaultMemberMode = DataMemberMode.Default)]
    [Display("Float32")]
    public class VoxelFragmentPackFloat32 : IVoxelFragmentPacker
    {
        ShaderSource source = new ShaderClassSource("VoxelFragmentPackFloat32");

        public ShaderSource GetShader() => source;

        public int GetBits(int channels) => channels * 32;
    }
}
