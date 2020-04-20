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

namespace Stride.Rendering.Compositing
{
    [DataContract]public partial class MSAAResolverParams : ShaderMixinParameters
    {
        public static readonly PermutationParameterKey<int> MSAASamples = ParameterKeys.NewPermutation<int>();
        public static readonly PermutationParameterKey<int> ResolveFilterType = ParameterKeys.NewPermutation<int>();
        public static readonly PermutationParameterKey<float> ResolveFilterDiameter = ParameterKeys.NewPermutation<float>();
    };
    internal static partial class ShaderMixins
    {
        internal partial class MSAAResolverEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                mixin.AddMacro("INPUT_MSAA_SAMPLES", context.GetParam(MSAAResolverParams.MSAASamples));
                context.Mixin(mixin, "MSAAResolverShader", context.GetParam(MSAAResolverParams.MSAASamples), context.GetParam(MSAAResolverParams.ResolveFilterType), context.GetParam(MSAAResolverParams.ResolveFilterDiameter));
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("MSAAResolverEffect", new MSAAResolverEffect());
            }
        }
    }
    internal static partial class ShaderMixins
    {
        internal partial class MSAADepthResolverEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                mixin.AddMacro("INPUT_MSAA_SAMPLES", context.GetParam(MSAAResolverParams.MSAASamples));
                context.Mixin(mixin, "MSAADepthResolverShader");
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("MSAADepthResolverEffect", new MSAADepthResolverEffect());
            }
        }
    }
}
