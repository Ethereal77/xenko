// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace Stride.Core.IO
{
    /// <summary>
    /// File access equivalent of <see cref="System.IO.FileAccess"/>.
    /// </summary>
    [Flags]
    public enum VirtualFileAccess : uint
    {
        /// <summary>
        /// Read access.
        /// </summary>
        Read = 1,

        /// <summary>
        /// Write access.
        /// </summary>
        Write = 2,

        /// <summary>
        /// Read/Write Access,
        /// </summary>
        ReadWrite = Read | Write,
    }
}
