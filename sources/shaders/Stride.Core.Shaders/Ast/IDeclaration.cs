// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Stride.Core.Shaders.Ast
{
    /// <summary>
    /// Toplevel interface for a declaration.
    /// </summary>
    public interface IDeclaration
    {
        /// <summary>
        ///   Gets or sets the name of this declaration
        /// </summary>
        /// <value>
        ///   The name.
        /// </value>
        Identifier Name { get; set; }
    }
}
