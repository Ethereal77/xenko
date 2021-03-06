// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

namespace Stride.Core
{
    /// <summary>
    /// Specifies the style used for textual serialization when an array/list or a dictionary/map must
    /// be serialized.
    /// </summary>
    public enum DataStyle
    {
        /// <summary>
        /// Let the emitter choose the style.
        /// </summary>
        Any,

        /// <summary>
        /// The normal style (One line per item, structured by space).
        /// </summary>
        Normal,

        /// <summary>
        /// The compact style (style embraced by [] or {})
        /// </summary>
        Compact,
    }
}
