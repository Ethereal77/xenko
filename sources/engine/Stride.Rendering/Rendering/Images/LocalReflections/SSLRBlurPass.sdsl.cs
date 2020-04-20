// <auto-generated>
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

namespace Stride.Rendering.Images
{
    public static partial class SSLRBlurPassKeys
    {
    }
    internal static partial class ShaderMixins
    {
        internal partial class SSLRBlurPassEffectH  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                mixin.AddMacro("CONVOLVE_VERTICAL", 0);
                context.Mixin(mixin, "SSLRBlurPass");
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("SSLRBlurPassEffectH", new SSLRBlurPassEffectH());
            }
        }
    }
    internal static partial class ShaderMixins
    {
        internal partial class SSLRBlurPassEffectV  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                mixin.AddMacro("CONVOLVE_VERTICAL", 1);
                context.Mixin(mixin, "SSLRBlurPass");
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("SSLRBlurPassEffectV", new SSLRBlurPassEffectV());
            }
        }
    }
}
