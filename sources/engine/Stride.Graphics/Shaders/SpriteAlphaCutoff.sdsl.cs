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

namespace Stride.Rendering
{
    public static partial class SpriteAlphaCutoffKeys
    {
    }
}
namespace Stride.Rendering
{
    internal static partial class ShaderMixins
    {
        internal partial class SpriteAlphaCutoffEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "SpriteAlphaCutoff", context.GetParam(SpriteBaseKeys.ColorIsSRgb));
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("SpriteAlphaCutoffEffect", new SpriteAlphaCutoffEffect());
            }
        }
    }
}
