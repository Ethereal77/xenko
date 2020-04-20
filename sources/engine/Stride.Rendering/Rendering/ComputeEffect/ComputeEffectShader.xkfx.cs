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

namespace Stride.Rendering.ComputeEffect
{
    internal static partial class ShaderMixins
    {
        internal partial class ComputeEffectShader  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                mixin.AddMacro("ThreadNumberX", context.GetParam(ComputeEffectShaderKeys.ThreadNumbers).X);
                mixin.AddMacro("ThreadNumberY", context.GetParam(ComputeEffectShaderKeys.ThreadNumbers).Y);
                mixin.AddMacro("ThreadNumberZ", context.GetParam(ComputeEffectShaderKeys.ThreadNumbers).Z);
                context.Mixin(mixin, "ComputeShaderBase");
                context.Mixin(mixin, context.GetParam(ComputeEffectShaderKeys.ComputeShaderName));
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("ComputeEffectShader", new ComputeEffectShader());
            }
        }
    }
}
