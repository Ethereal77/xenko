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

using Xenko.Rendering.Materials;
namespace Xenko.Rendering.Shadows
{
    internal static partial class ShaderMixins
    {
        internal partial class ShadowMapCasterCubeMap  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                if (context.GetParam(MaterialKeys.UsePixelShaderWithDepthPass))
                {
                    context.Mixin(mixin, "ShadowMapCasterAlphaDiscard");
                }
                else
                {
                    context.Mixin(mixin, "ShadowMapCasterNoPixelShader");
                }
                context.Mixin(mixin, "ShadowMapCasterCubeMapProjection");
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("ShadowMapCasterCubeMap", new ShadowMapCasterCubeMap());
            }
        }
    }
}
