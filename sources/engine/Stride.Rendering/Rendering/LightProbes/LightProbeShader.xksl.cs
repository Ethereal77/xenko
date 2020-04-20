﻿// <auto-generated>
// Do not edit this file yourself!
//
// This code was generated by Stride Shader Mixin Code Generator.
// To generate it yourself, please install Stride.VisualStudio.Package .vsix
// and re-save the associated .xkfx.
// </auto-generated>

using System;

using Stride.Core;
using Stride.Rendering;
using Stride.Graphics;
using Stride.Shaders;
using Stride.Core.Mathematics;
using Buffer = Stride.Graphics.Buffer;

namespace Stride.Rendering.LightProbes
{
    public static partial class LightProbeShaderKeys
    {
        public static readonly ValueParameterKey<int> IgnoredProbeStart = ParameterKeys.NewValue<int>();
        public static readonly ObjectParameterKey<Texture> LightProbeTetrahedronIds = ParameterKeys.NewObject<Texture>();
        public static readonly ObjectParameterKey<Buffer> LightProbeTetrahedronProbeIndices = ParameterKeys.NewObject<Buffer>();
        public static readonly ObjectParameterKey<Buffer> LightProbeTetrahedronMatrices = ParameterKeys.NewObject<Buffer>();
        public static readonly ObjectParameterKey<Buffer> LightProbeCoefficients = ParameterKeys.NewObject<Buffer>();
    }
}
