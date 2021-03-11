// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2019 Sean Boettger <sean@whypenguins.com>
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.Generic;

using Stride.Core;
using Stride.Shaders;

namespace Stride.Rendering.Voxels
{
    [DataContract(DefaultMemberMode = DataMemberMode.Default)]
    [Display("Solidify")]
    public class VoxelModifierEmissionOpacitySolidify : VoxelModifierEmissionOpacity
    {
        VoxelAttributeSolidity solidityAttribute = new VoxelAttributeSolidity();

        public override void CollectAttributes(List<AttributeStream> attributes, VoxelizationStage stage, bool output)
        {
            solidityAttribute.CollectAttributes(attributes, stage, output);
        }

        public override bool RequiresColumns() => true;

        public override ShaderSource GetApplier(string layout)
        {
            return new ShaderClassSource("VoxelModifierApplierSolidify" + layout, solidityAttribute.LocalSamplerID);
        }

        public override void UpdateVoxelizationLayout(string compositionName) { }

        public override void ApplyVoxelizationParameters(ParameterCollection parameters) { }
    }
}
