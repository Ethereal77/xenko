// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2015 SharpYaml - Alexandre Mutel
// Copyright (c) 2008-2012 YamlDotNet - Antoine Aubry
// See the LICENSE.md file in the project root for full license information.

namespace Stride.Core.Yaml.Tokens
{
    /// <summary>
    /// Represents an anchor token.
    /// </summary>
    public class Anchor : Token
    {
        private readonly string value;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get { return value; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Anchor"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Anchor(string value)
            : this(value, Mark.Empty, Mark.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Anchor"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="start">The start position of the token.</param>
        /// <param name="end">The end position of the token.</param>
        public Anchor(string value, Mark start, Mark end)
            : base(start, end)
        {
            this.value = value;
        }
    }
}