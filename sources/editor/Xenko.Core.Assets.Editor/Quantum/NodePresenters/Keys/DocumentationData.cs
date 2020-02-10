// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core;

namespace Xenko.Core.Assets.Editor.Quantum.NodePresenters.Keys
{
    public static class DocumentationData
    {
        public const string Documentation = nameof(Documentation);

        public static readonly PropertyKey<string> Key = new PropertyKey<string>(Documentation, typeof(DocumentationData));
    }
}
