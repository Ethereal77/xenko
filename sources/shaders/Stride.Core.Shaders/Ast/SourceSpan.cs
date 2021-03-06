// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Core;

namespace Stride.Core.Shaders.Ast
{
    /// <summary>
    /// A SourceSpan.
    /// </summary>
    [DataContract]
    public struct SourceSpan
    {
        #region Constants and Fields

        /// <summary>
        /// Location of this span.
        /// </summary>
        public SourceLocation Location;

        /// <summary>
        /// Length of this span.
        /// </summary>
        public int Length;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceSpan"/> struct.
        /// </summary>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        public SourceSpan(SourceLocation location, int length)
        {
            Location = location;
            Length = length;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format("{0}", Location);
        }

        #endregion
    }
}
