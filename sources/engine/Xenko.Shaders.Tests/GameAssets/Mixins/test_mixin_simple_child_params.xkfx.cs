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

namespace Test4
{
    [DataContract]public partial class TestParameters : ShaderMixinParameters
    {
        public static readonly PermutationParameterKey<int> TestCount = ParameterKeys.NewPermutation<int>();
        public static readonly PermutationParameterKey<bool> UseComputeColorEffect = ParameterKeys.NewPermutation<bool>();
    };
    internal static partial class ShaderMixins
    {
        internal partial class ChildParamsMixin  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.SetParam(TestParameters.TestCount, 1);
                if (context.GetParam(TestParameters.TestCount) == 1)
                    context.Mixin(mixin, "C1");
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("ChildParamsMixin", new ChildParamsMixin());
            }
        }
    }
    internal static partial class ShaderMixins
    {
        internal partial class DefaultSimpleChildParams  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "A");
                if (context.GetParam(TestParameters.TestCount) == 0)
                    context.Mixin(mixin, "B");
                if (context.ChildEffectName == "ChildParamsMixin")
                {
                    context.Mixin(mixin, "ChildParamsMixin");
                    return;
                }
                if (context.GetParam(TestParameters.TestCount) == 0)
                    context.Mixin(mixin, "C");
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("DefaultSimpleChildParams", new DefaultSimpleChildParams());
            }
        }
    }
}
