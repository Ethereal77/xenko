// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core.Reflection;

namespace Stride.Core.Assets
{
    // TODO: at some point we should converge to a state where collection ids, which are for override and asset specific, should move from Core.Design to this assembly (with related yaml serializer). Meanwhile, we have to split some of the logic in an unclean manner.
    public static class AssetCollectionItemIdHelper
    {
        public static void GenerateMissingItemIds(object rootObject)
        {
            var visitor = new CollectionIdGenerator();
            visitor.Visit(rootObject);
        }
    }
}
