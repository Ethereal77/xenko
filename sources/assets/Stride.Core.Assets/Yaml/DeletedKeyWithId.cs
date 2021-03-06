// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Core.Reflection;

namespace Stride.Core.Yaml
{
    /// <summary>
    /// A generic structure that implements the <see cref="IKeyWithId"/> interface for keys that are deleted.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public struct DeletedKeyWithId<TKey> : IKeyWithId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyWithId{TKey}"/> structure.
        /// </summary>
        /// <param name="id">The <see cref="ItemId"/> associated to the deleted key.</param>
        public DeletedKeyWithId(ItemId id)
        {
            Id = id;
        }

        /// <summary>
        /// The <see cref="ItemId"/> associated to the key.
        /// </summary>
        public readonly ItemId Id;
        /// <inheritdoc/>
        ItemId IKeyWithId.Id => Id;
        /// <inheritdoc/>
        object IKeyWithId.Key => default(TKey);
        /// <inheritdoc/>
        bool IKeyWithId.IsDeleted => true;
        /// <inheritdoc/>
        Type IKeyWithId.KeyType => typeof(TKey);
    }
}
