// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core;

namespace Stride.Core.Assets.Editor.Quantum.NodePresenters.Keys
{
    public static class DocumentationData
    {
        public const string Documentation = nameof(Documentation);

        public static readonly PropertyKey<string> Key = new PropertyKey<string>(Documentation, typeof(DocumentationData));
    }
}
