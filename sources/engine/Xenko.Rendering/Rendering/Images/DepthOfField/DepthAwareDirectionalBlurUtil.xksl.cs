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

namespace Xenko.Rendering.Images
{
    public static partial class DepthAwareDirectionalBlurUtilKeys
    {
        public static readonly ValueParameterKey<Vector2> Direction = ParameterKeys.NewValue<Vector2>();
        public static readonly ValueParameterKey<float> Radius = ParameterKeys.NewValue<float>();
        public static readonly ValueParameterKey<float> TapWeights = ParameterKeys.NewValue<float>();
        public static readonly ValueParameterKey<float> CoCReference = ParameterKeys.NewValue<float>();
    }
}
