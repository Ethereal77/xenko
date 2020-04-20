// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

using Stride.Core.Annotations;

namespace Stride.Core.Assets.Compiler
{
    /// <summary>
    /// Attribute to define an asset compiler for a <see cref="Asset"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    [BaseTypeRequired(typeof(IAssetCompiler))]
    public class AssetCompilerAttribute : DynamicTypeAttributeBase
    {
        public Type CompilationContext { get; private set; }

        public AssetCompilerAttribute(Type type, Type compilationContextType)
            : base(type)
        {
            CompilationContext = compilationContextType;
        }

        public AssetCompilerAttribute(string typeName, Type compilationContextType)
            : base(typeName)
        {
            CompilationContext = compilationContextType;
        }
    }
}
