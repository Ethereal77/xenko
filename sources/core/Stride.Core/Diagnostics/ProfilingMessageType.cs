// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Stride.Core.Diagnostics
{
    /// <summary>
    /// Type of a profiling message.
    /// </summary>
    public enum ProfilingMessageType
    {
        /// <summary>
        /// A begin message.
        /// </summary>
        Begin,

        /// <summary>
        /// A end message.
        /// </summary>
        End,

        /// <summary>
        /// A mark message.
        /// </summary>
        Mark,
    }
}
