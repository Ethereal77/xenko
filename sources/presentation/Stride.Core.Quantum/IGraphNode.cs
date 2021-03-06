// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;

using Stride.Core.Annotations;
using Stride.Core.Reflection;

namespace Stride.Core.Quantum
{
    /// <summary>
    /// The <see cref="IGraphNode"/> interface represents a node in a Quantum object graph. This node can represent an object or a member of an object.
    /// </summary>
    public interface IGraphNode
    {
        /// <summary>
        /// Gets or sets the <see cref="System.Guid"/>.
        /// </summary>
        Guid Guid { get; }

        /// <summary>
        /// Gets the expected type of for the content of this node.
        /// </summary>
        /// <remarks>The actual type of the content can be different, for example it could be a type inheriting from this type.</remarks>
        [NotNull]
        Type Type { get; }

        /// <summary>
        /// Gets or sets the type descriptor of this content
        /// </summary>
        [NotNull]
        ITypeDescriptor Descriptor { get; }

        /// <summary>
        /// Gets wheither this node holds a reference or is a direct value.
        /// </summary>
        bool IsReference { get; }

        /// <summary>
        /// Retrieves the value of this node.
        /// </summary>
        object Retrieve();

        /// <summary>
        /// Retrieves the value of one of the item of this node, if it holds a collection.
        /// </summary>
        /// <param name="index">The index to use to retrieve the value.</param>
        object Retrieve(NodeIndex index);
    }
}
