﻿// <auto-generated>
// Do not edit this file yourself!
//
// This code was generated by Stride Shader Mixin Code Generator.
// To generate it yourself, please install Stride.VisualStudio.Package .vsix
// and re-save the associated .sdfx.
// </auto-generated>

using System;

using Stride.Core;
using Stride.Rendering;
using Stride.Graphics;
using Stride.Shaders;
using Stride.Core.Mathematics;
using Buffer = Stride.Graphics.Buffer;

namespace Stride.Rendering.Shadows
{
    internal static partial class ShadowMapReceiverPointParaboloidKeys
    {
        public static readonly ValueParameterKey<Matrix> View = ParameterKeys.NewValue<Matrix>();
        public static readonly ValueParameterKey<Vector2> FaceOffsets = ParameterKeys.NewValue<Vector2>();
        public static readonly ValueParameterKey<Vector2> BackfaceOffsets = ParameterKeys.NewValue<Vector2>();
        public static readonly ValueParameterKey<Vector2> FaceSizes = ParameterKeys.NewValue<Vector2>();
        public static readonly ValueParameterKey<float> DepthBiases = ParameterKeys.NewValue<float>();
        public static readonly ValueParameterKey<Vector2> DepthParameters = ParameterKeys.NewValue<Vector2>();
    }
}
