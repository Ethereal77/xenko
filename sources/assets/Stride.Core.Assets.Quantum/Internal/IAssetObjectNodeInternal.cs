// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Collections.Generic;

using Stride.Core.Reflection;
using Stride.Core.Quantum;

namespace Stride.Core.Assets.Quantum.Internal
{
    /// <summary>
    /// An interface exposing internal methods of <see cref="IAssetObjectNode"/>
    /// </summary>
    internal interface IAssetObjectNodeInternal : IAssetObjectNode, IAssetNodeInternal
    {
        OverrideType GetItemOverride(NodeIndex index);

        OverrideType GetKeyOverride(NodeIndex index);

        /// <summary>
        /// Removes the given <see cref="ItemId"/> from the list of overridden deleted items in the underlying <see cref="CollectionItemIdentifiers"/>, but keep
        /// track of it if this node is requested whether this id is overridden-deleted.
        /// </summary>
        /// <param name="deletedId">The id to disconnect.</param>
        /// <remarks>The purpose of this method is to unmark as deleted the given id, but keep track of it for undo-redo.</remarks>
        void DisconnectOverriddenDeletedItem(ItemId deletedId);

        void NotifyOverrideChanging();

        void NotifyOverrideChanged();
    }
}
