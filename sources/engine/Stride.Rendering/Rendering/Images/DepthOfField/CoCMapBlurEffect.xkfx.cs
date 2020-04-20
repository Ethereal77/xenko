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

namespace Stride.Rendering.Images
{
    internal static partial class ShaderMixins
    {
        internal partial class CoCMapBlurEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "CoCMapBlurShader", context.GetParam(DepthAwareDirectionalBlurKeys.Count));
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("CoCMapBlurEffect", new CoCMapBlurEffect());
            }
        }
    }
}
