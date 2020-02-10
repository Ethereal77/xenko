// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;

using Xenko.Core.Serialization;

using Xunit;

namespace Xenko.Core.Assets.Tests.Compilers
{
    public class CompilerTestBase : IDisposable
    {
        public CompilerTestBase()
        {
            TestCompilerBase.CompiledAssets = new HashSet<AssetItem>();
        }

        public void Dispose()
        {
            TestCompilerBase.CompiledAssets = null;
        }

        protected static TContentType CreateRef<TContentType>(AssetItem assetItem) where TContentType : class, new()
        {
            return AttachedReferenceManager.CreateProxyObject<TContentType>(assetItem.Id, assetItem.Location);
        }
    }
}
