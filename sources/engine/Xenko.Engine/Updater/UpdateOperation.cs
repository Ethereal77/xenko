// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Xenko.Updater
{
    /// <summary>
    /// Defines an update operation for internal use by <see cref="UpdateEngine"/>.
    /// </summary>
    internal struct UpdateOperation
    {
        internal UpdateOperationType Type;
        internal UpdatableMember Member;
        internal EnterChecker EnterChecker;

        // TODO: Should we switch to short + short? (note: could be a problem with big arrays)

        // Apply an offset to current object pointer.
        public int AdjustOffset;

        // Note: It is either an offset (blittable struct) or an index into object array (reference types and non blittable struct)
        public int DataOffset;

        // Number of operations to skip if this is null
        public int SkipCountIfNull;
    
        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
