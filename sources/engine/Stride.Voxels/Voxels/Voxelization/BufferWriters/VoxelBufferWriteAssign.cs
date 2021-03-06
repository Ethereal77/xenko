// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2019 Sean Boettger <sean@whypenguins.com>
// See the LICENSE.md file in the project root for full license information.

using Stride.Core;
using Stride.Shaders;

namespace Stride.Rendering.Voxels
{
    //[DataContract("VoxelFlickerReductionNone")]
    [DataContract(DefaultMemberMode = DataMemberMode.Default)]
    [Display("None")]
    public class VoxelBufferWriteAssign : IVoxelBufferWriter
    {
        ShaderSource source = new ShaderClassSource("VoxelBufferWriteAssign");

        public ShaderSource GetShader() => source;
    }
}
