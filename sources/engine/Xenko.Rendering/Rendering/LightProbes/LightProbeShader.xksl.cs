﻿// <auto-generated>
// Do not edit this file yourself!
//
// This code was generated by Xenko Shader Mixin Code Generator.
// To generate it yourself, please install Xenko.VisualStudio.Package .vsix
// and re-save the associated .xkfx.
// </auto-generated>

using System;

using Xenko.Core;
using Xenko.Rendering;
using Xenko.Graphics;
using Xenko.Shaders;
using Xenko.Core.Mathematics;
using Buffer = Xenko.Graphics.Buffer;

namespace Xenko.Rendering.LightProbes
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
