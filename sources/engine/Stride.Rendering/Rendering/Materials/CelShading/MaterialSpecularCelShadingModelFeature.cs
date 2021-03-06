// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Core;
using Stride.Core.Annotations;
using Stride.Shaders;

namespace Stride.Rendering.Materials
{
    /// <summary>
    /// The Cel Shading specular shading model.
    /// </summary>
    [DataContract("MaterialSpecularCelShadingModelFeature")]
    [Display("Cel Shading")]
    public class MaterialSpecularCelShadingModelFeature : MaterialSpecularMicrofacetModelFeature, IEquatable<MaterialSpecularCelShadingModelFeature>
    {
        [DataMember(5)]
        [Display("Ramp Function")]
        [NotNull]
        public IMaterialCelShadingLightFunction RampFunction { get; set; } = new MaterialCelShadingLightDefault();

        public override void GenerateShader(MaterialGeneratorContext context)
        {
            var shaderSource = new ShaderMixinSource();
            shaderSource.Mixins.Add(new ShaderClassSource("MaterialSurfaceShadingSpecularCelShading"));

            GenerateShaderCompositions(context, shaderSource);

            var shaderBuilder = context.AddShading(this);
            shaderBuilder.LightDependentSurface = shaderSource;
        }

        protected override void GenerateShaderCompositions(MaterialGeneratorContext context, ShaderMixinSource shaderSource)
        {
            base.GenerateShaderCompositions(context, shaderSource);

            if (RampFunction != null)
            {
                shaderSource.AddComposition("celLightFunction", RampFunction.Generate(context));
            }
        }

        public bool Equals(MaterialSpecularCelShadingModelFeature other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && RampFunction.Equals(other.RampFunction);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MaterialSpecularCelShadingModelFeature)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (RampFunction != null ? RampFunction.GetHashCode() : 0);
            }
        }
    }
}
