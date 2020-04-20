﻿// <auto-generated>
// Do not edit this file yourself!
//
// This code was generated by Stride Shader Mixin Code Generator.
// To generate it yourself, please install Stride.VisualStudio.Package .vsix
// and re-save the associated .sdfx.
// </auto-generated>

using System;

using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Rendering;
using Stride.Graphics;
using Stride.Shaders;

using Buffer = Stride.Graphics.Buffer;

namespace Stride.Rendering
{
    public static partial class TestComputeShaderKeys
    {
        public static readonly ValueParameterKey<Vector3> ThreadGroupCountGlobal = ParameterKeys.NewValue<Vector3>();
        public static readonly ValueParameterKey<uint> ParticleCount = ParameterKeys.NewValue<uint>();
        public static readonly ValueParameterKey<uint> ParticleStartIndex = ParameterKeys.NewValue<uint>();
        public static readonly ObjectParameterKey<Buffer> ParticleSortBuffer = ParameterKeys.NewObject<Buffer>();
    }
}
