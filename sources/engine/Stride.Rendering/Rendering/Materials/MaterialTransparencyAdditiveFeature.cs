// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core;
using Stride.Core.Annotations;
using Stride.Core.Mathematics;
using Stride.Graphics;
using Stride.Shaders;
using Stride.Rendering.Materials.ComputeColors;

namespace Stride.Rendering.Materials
{
    /// <summary>
    ///   Represents a transparent additive material.
    /// </summary>
    [DataContract("MaterialTransparencyAdditiveFeature")]
    [Display("Additive")]
    public class MaterialTransparencyAdditiveFeature : MaterialFeature, IMaterialTransparencyFeature
    {
        private static readonly MaterialStreamDescriptor AlphaBlendStream = new MaterialStreamDescriptor("DiffuseSpecularAlphaBlend", "matDiffuseSpecularAlphaBlend", MaterialKeys.DiffuseSpecularAlphaBlendValue.PropertyType);
        private static readonly MaterialStreamDescriptor AlphaBlendColorStream = new MaterialStreamDescriptor("DiffuseSpecularAlphaBlend - Color", "matAlphaBlendColor", MaterialKeys.AlphaBlendColorValue.PropertyType);

        private static readonly PropertyKey<bool> HasFinalCallback = new PropertyKey<bool>("MaterialTransparencyAdditiveFeature.HasFinalCallback", typeof(MaterialTransparencyAdditiveFeature));

        /// <summary>
        ///   Initializes a new instance of the <see cref="MaterialTransparencyAdditiveFeature"/> class.
        /// </summary>
        public MaterialTransparencyAdditiveFeature()
        {
            Alpha = new ComputeFloat(0.5f);
            Tint = new ComputeColor(Color.White);
        }

        /// <summary>
        ///   Gets or sets the alpha (oppacity) of the material.
        /// </summary>
        /// <value>The alpha value.</value>
        /// <userdoc>The factor used to modulate alpha of the material. See documentation for more details.</userdoc>
        [NotNull]
        [DataMember(10)]
        [DataMemberRange(0.0, 1.0, 0.01, 0.1, 2)]
        public IComputeScalar Alpha { get; set; }

        /// <summary>
        ///   Gets or sets the tint color.
        /// </summary>
        /// <value>The tint.</value>
        /// <userdoc>The tint color to apply on the material during the blend.</userdoc>
        [NotNull]
        [DataMember(20)]
        public IComputeColor Tint { get; set; }

        public override void GenerateShader(MaterialGeneratorContext context)
        {
            var alpha = Alpha ?? new ComputeFloat(0.5f);
            var tint = Tint ?? new ComputeColor(Color.White);

            alpha.ClampFloat(0, 1);

            // Use pre-multiplied alpha to support both additive and alpha blending
            if (context.MaterialPass.BlendState is null)
                context.MaterialPass.BlendState = BlendStates.AlphaBlend;

            context.MaterialPass.HasTransparency = true;

            // Disable alpha-to-coverage. We want to do alpha blending, not alpha testing
            context.MaterialPass.AlphaToCoverage = false;

            // TODO: GRAPHICS REFACTOR
            //context.Parameters.SetResourceSlow(Effect.BlendStateKey, BlendState.NewFake(blendDesc));

            var alphaColor = alpha.GenerateShaderSource(context, new MaterialComputeColorKeys(MaterialKeys.DiffuseSpecularAlphaBlendMap, MaterialKeys.DiffuseSpecularAlphaBlendValue, Color.White));

            var mixin = new ShaderMixinSource();
            mixin.Mixins.Add(new ShaderClassSource("ComputeColorMaterialAlphaBlend"));
            mixin.AddComposition("color", alphaColor);

            context.SetStream(MaterialShaderStage.Pixel, AlphaBlendStream.Stream, MaterialStreamType.Float2, mixin);
            context.SetStream(AlphaBlendColorStream.Stream, tint, MaterialKeys.AlphaBlendColorMap, MaterialKeys.AlphaBlendColorValue, Color.White);

            context.MaterialPass.Parameters.Set(MaterialKeys.UsePixelShaderWithDepthPass, true);

            if (!context.Tags.Get(HasFinalCallback))
            {
                context.Tags.Set(HasFinalCallback, true);
                context.AddFinalCallback(MaterialShaderStage.Pixel, AddDiffuseSpecularAlphaBlendColor);
            }
        }

        private void AddDiffuseSpecularAlphaBlendColor(MaterialShaderStage stage, MaterialGeneratorContext context)
        {
            context.AddShaderSource(MaterialShaderStage.Pixel, new ShaderClassSource("MaterialSurfaceDiffuseSpecularAlphaBlendColor"));
        }
    }
}
