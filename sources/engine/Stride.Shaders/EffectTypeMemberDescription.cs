// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Diagnostics;

using Stride.Core;

namespace Stride.Shaders
{
    /// <summary>
    /// Describes a shader parameter member.
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{Name}: {Type}")]
    public struct EffectTypeMemberDescription
    {
        /// <summary>
        /// The name of this member.
        /// </summary>
        public string Name;

        /// <summary>
        /// Offset in bytes into the parent structure (0 if not a structure member).
        /// </summary>
        public int Offset;

        /// <summary>
        /// The type of this member.
        /// </summary>
        public EffectTypeDescription Type;
    }
}
