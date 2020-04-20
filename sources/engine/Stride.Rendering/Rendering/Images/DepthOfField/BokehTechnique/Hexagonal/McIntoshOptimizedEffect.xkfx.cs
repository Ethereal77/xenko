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
        internal partial class McIntoshOptimizedEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "McIntoshOptimizedShader");

                {
                    var __mixinToCompose__ = "DepthAwareDirectionalBlurUtil";
                    var __subMixin = new ShaderMixinSource();
                    context.PushComposition(mixin, "directionalBlurA", __subMixin);
                    context.Mixin(__subMixin, __mixinToCompose__, context.GetParam(DepthAwareDirectionalBlurKeys.Count), context.GetParam(DepthAwareDirectionalBlurKeys.TotalTap));
                    context.PopComposition();
                }

                {
                    var __mixinToCompose__ = "DepthAwareDirectionalBlurUtil";
                    var __subMixin = new ShaderMixinSource();
                    context.PushComposition(mixin, "directionalBlurB", __subMixin);
                    context.Mixin(__subMixin, __mixinToCompose__, context.GetParam(DepthAwareDirectionalBlurKeys.Count), context.GetParam(DepthAwareDirectionalBlurKeys.TotalTap));
                    context.PopComposition();
                }
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("McIntoshOptimizedEffect", new McIntoshOptimizedEffect());
            }
        }
    }
}
