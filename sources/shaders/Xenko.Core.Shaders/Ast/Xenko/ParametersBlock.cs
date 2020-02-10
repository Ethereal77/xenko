// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.Generic;

using Xenko.Core.Shaders.Ast;

namespace Xenko.Core.Shaders.Ast.Xenko
{
    /// <summary>
    /// A params block.
    /// </summary>
    public partial class ParametersBlock : Node, IScopeContainer
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "ParametersBlock" /> class.
        /// </summary>
        public ParametersBlock()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParametersBlock" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="statements">The statements.</param>
        public ParametersBlock(Identifier name, BlockStatement statements)
        {
            Name = name;
            Body = statements;
        }

        #endregion

        #region Public Properties

        public Identifier Name { get; set; }

        public BlockStatement Body { get; set; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override IEnumerable<Node> Childrens()
        {
            ChildrenList.Clear();
            ChildrenList.Add(Name);
            ChildrenList.Add(Body);
            return ChildrenList;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("params {0} {{...}}", Name);
        }

        #endregion
    }
}
