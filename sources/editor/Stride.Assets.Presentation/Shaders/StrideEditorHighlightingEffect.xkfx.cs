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

using Stride.Rendering.Data;
using Stride.Shaders.Compiler;
namespace StrideEffects
{
    internal static partial class ShaderMixins
    {
        internal partial class StrideEditorHighlightingEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "StrideForwardShadingEffect");
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("StrideEditorHighlightingEffect", new StrideEditorHighlightingEffect());
            }
        }
    }
}
