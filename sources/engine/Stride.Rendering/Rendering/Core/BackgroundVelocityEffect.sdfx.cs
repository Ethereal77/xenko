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

internal static partial class ShaderMixins
{
    internal partial class BackgroundVelocityEffect  : IShaderMixinBuilder
    {
        public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
        {
            context.Mixin(mixin, "ShaderBase");
            context.Mixin(mixin, "ShadingBase");
            context.Mixin(mixin, "BackgroundVelocity");
            var targetExtensions = context.GetParam(StrideEffectBaseKeys.RenderTargetExtensions);
            if (targetExtensions != null)
            {
                context.Mixin(mixin, (targetExtensions));
            }
        }

        [ModuleInitializer]
        internal static void __Initialize__()

        {
            ShaderMixinManager.Register("BackgroundVelocityEffect", new BackgroundVelocityEffect());
        }
    }
}
