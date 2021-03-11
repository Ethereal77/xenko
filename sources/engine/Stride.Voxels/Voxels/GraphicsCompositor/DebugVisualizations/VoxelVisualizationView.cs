// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2019 Sean Boettger <sean@whypenguins.com>
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.


using Stride.Core;
using Stride.Core.Annotations;
using Stride.Core.Mathematics;
using Stride.Rendering.Images;
using Stride.Shaders;

namespace Stride.Rendering.Voxels.Debug
{
    [DataContract(DefaultMemberMode = DataMemberMode.Default)]
    [Display("Standard")]
    public class VoxelVisualizationView : IVoxelVisualization
    {
        [NotNull]
        public IVoxelMarchMethod MarchMethod { get; set; } = new VoxelMarchBeam(200, 1.0f, 1.0f);

        public Color Background = new Color(0.1f, 0.1f, 0.1f, 1.0f);

        private ImageEffectShader voxelDebugEffectShader = new ImageEffectShader("VoxelVisualizationViewEffect");


        public ImageEffectShader GetShader(RenderDrawContext context, VoxelAttribute attr)
        {
            VoxelViewContext viewContext = new VoxelViewContext(voxelView: false);

            Matrix ViewProjection = context.RenderContext.RenderView.ViewProjection;

            voxelDebugEffectShader.Parameters.Set(VoxelVisualizationViewShaderKeys.view, ViewProjection);
            voxelDebugEffectShader.Parameters.Set(VoxelVisualizationViewShaderKeys.viewInv, Matrix.Invert(ViewProjection));
            voxelDebugEffectShader.Parameters.Set(VoxelVisualizationViewShaderKeys.background, (Vector4) Background);

            attr.UpdateSamplingLayout("AttributeSamplers[0]");
            attr.ApplySamplingParameters(viewContext, voxelDebugEffectShader.Parameters);
            MarchMethod.UpdateMarchingLayout("marcher");
            MarchMethod.ApplyMarchingParameters(voxelDebugEffectShader.Parameters);
            voxelDebugEffectShader.Parameters.Set(VoxelVisualizationViewShaderKeys.marcher, MarchMethod.GetMarchingShader(0));

            ShaderSourceCollection collection = new ShaderSourceCollection
            {
                attr.GetSamplingShader()
            };
            voxelDebugEffectShader.Parameters.Set(MarchAttributesKeys.AttributeSamplers, collection);

            return voxelDebugEffectShader;
        }
    }
}
