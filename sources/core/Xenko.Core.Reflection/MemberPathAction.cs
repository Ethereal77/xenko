// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace Xenko.Core.Reflection
{
    /// <summary>
    /// A type of action used by <see cref="MemberPath.Apply"/>
    /// </summary>
    public enum MemberPathAction
    {
        /// <summary>
        /// The value is set on the <see cref="MemberPath"/> (field/property setter, or new key for dictionary or index
        /// for collection/array)
        /// </summary>
        ValueSet,

        /// <summary>
        /// Removes a key from the dictionary
        /// </summary>
        DictionaryRemove,

        /// <summary>
        /// Adds a value to the collection.
        /// </summary>
        CollectionAdd,

        /// <summary>
        /// Removes a value from the collection
        /// </summary>
        CollectionRemove,

        /// <summary>
        /// Clears the value.
        /// </summary>
        ValueClear,
    }
}
