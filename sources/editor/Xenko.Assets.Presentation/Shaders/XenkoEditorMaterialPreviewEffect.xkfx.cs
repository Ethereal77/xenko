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

using Xenko.Rendering.Data;
using Xenko.Shaders.Compiler;
namespace XenkoEffects
{
    internal static partial class ShaderMixins
    {
        internal partial class XenkoEditorMaterialPreviewEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "XenkoEditorForwardShadingEffect");
                context.Mixin(mixin, "SharedTextureCoordinate");
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("XenkoEditorMaterialPreviewEffect", new XenkoEditorMaterialPreviewEffect());
            }
        }
    }
}
