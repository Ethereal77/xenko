// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections;
using System.Linq;

using Stride.Irony.Parsing;

namespace Stride.Core.Shaders.Ast
{
    /// <summary>
    /// Internal class to provides <see cref="Node"/> class browsable by Irony.
    /// </summary>
    internal class IronyBrowsableNode : IBrowsableAstNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IronyBrowsableNode"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        public IronyBrowsableNode(Node node)
        {
            Node = node;
        }

        /// <inheritdoc/>
        public Irony.Parsing.SourceLocation Location
        {
            get
            {
                return new Irony.Parsing.SourceLocation
                    {
                        SourceFilename = Node.Span.Location.FileSource, 
                        Position = Node.Span.Location.Position, 
                        Line = Node.Span.Location.Line, 
                        Column = Node.Span.Location.Column
                    };
            }
        }

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>
        /// The node.
        /// </value>
        public Node Node { get; set; }

        /// <inheritdoc/>
        public IEnumerable GetChildNodes()
        {
            return from children in Node.Childrens() where children != null select new IronyBrowsableNode(children);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Node.ToString();
        }
    }
}
