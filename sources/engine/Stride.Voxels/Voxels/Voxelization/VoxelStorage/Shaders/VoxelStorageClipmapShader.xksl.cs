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

namespace Stride.Rendering
{
    public static partial class VoxelStorageClipmapShaderKeys
    {
        public static readonly ValueParameterKey<Vector3> clipMapResolution = ParameterKeys.NewValue<Vector3>();
        public static readonly ValueParameterKey<float> clipPos = ParameterKeys.NewValue<float>();
        public static readonly ValueParameterKey<Vector3> clipScale = ParameterKeys.NewValue<Vector3>();
        public static readonly ValueParameterKey<Vector3> clipOffset = ParameterKeys.NewValue<Vector3>();
        public static readonly ValueParameterKey<float> clipMapCount = ParameterKeys.NewValue<float>();
        public static readonly ValueParameterKey<Vector4> perClipMapOffsetScale = ParameterKeys.NewValue<Vector4>();
        public static readonly ValueParameterKey<float> storageUints = ParameterKeys.NewValue<float>();
        public static readonly ObjectParameterKey<Buffer> fragmentsBuffer = ParameterKeys.NewObject<Buffer>();
    }
}
