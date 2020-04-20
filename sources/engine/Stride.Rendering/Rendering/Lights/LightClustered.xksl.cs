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

namespace Stride.Rendering.Lights
{
    public static partial class LightClusteredKeys
    {
        public static readonly ObjectParameterKey<Texture> LightClusters = ParameterKeys.NewObject<Texture>();
        public static readonly ObjectParameterKey<Buffer> LightIndices = ParameterKeys.NewObject<Buffer>();
        public static readonly ValueParameterKey<float> ClusterDepthScale = ParameterKeys.NewValue<float>();
        public static readonly ValueParameterKey<float> ClusterDepthBias = ParameterKeys.NewValue<float>();
        public static readonly ValueParameterKey<Vector2> ClusterStride = ParameterKeys.NewValue<Vector2>();
    }
}
