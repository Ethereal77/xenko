// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2019 Sean Boettger <sean@whypenguins.com>
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core;
using Stride.Shaders;

namespace Stride.Rendering.Voxels
{
    [DataContract(DefaultMemberMode = DataMemberMode.Default)]
    [Display("R11G11B10F")]
    public class VoxelFragmentPackFloatR11G11B10 : IVoxelFragmentPacker
    {
        ShaderSource source = new ShaderClassSource("VoxelFragmentPackFloatR11G11B10");

        public ShaderSource GetShader() => source;

        public int GetBits(int channels)
        {
            return 32 * ((channels + 2) / 3); // (channels)/3 * 32 + (channels % 3) * 11
        }
    }
}
