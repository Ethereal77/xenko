// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.ComponentModel;

using Stride.Core;

namespace Stride.Rendering.Materials.ComputeColors
{
    /// <summary>
    /// Base interface for all computer color nodes.
    /// </summary>
    [DataContract(Inherited = true)]
    public abstract class ComputeKeyedBase : ComputeNode
    {
        /// <summary>
        /// Gets or sets a custom key associated to this node.
        /// </summary>
        /// <value>The key.</value>
        [DataMemberIgnore]
        [DefaultValue(null)]
        public ParameterKey Key { get; set; }

        /// <summary>
        /// Gets or sets the used key.
        /// </summary>
        /// <value>The used key.</value>
        [DataMemberIgnore]
        public ParameterKey UsedKey { get; set; }
    }
}
