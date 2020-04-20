// <auto-generated>
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
    public static partial class SSLRResolvePassKeys
    {
    }
    internal static partial class ShaderMixins
    {
        internal partial class SSLRResolvePassEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "SSLRResolvePass", context.GetParam(SSLRKeys.ResolveSamples), context.GetParam(SSLRKeys.ReduceHighlights));
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("SSLRResolvePassEffect", new SSLRResolvePassEffect());
            }
        }
    }
}
