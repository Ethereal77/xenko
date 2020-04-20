// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2009 NShader - Alexandre Mutel, Microsoft Corporation
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;

namespace NShader
{
    /// <summary>
    /// Result of shader navigation.
    /// </summary>
    [Serializable]
    public class RawShaderNavigationResult
    {
        public RawShaderNavigationResult()
        {
            Messages = new List<RawShaderAnalysisMessage>();
        }

        /// <summary>
        /// Gets or sets the definition Span.
        /// </summary>
        /// <value>The definition Span.</value>
        public RawSourceSpan DefinitionSpan { get; set; }

        /// <summary>
        /// Gets the parsing messages.
        /// </summary>
        /// <value>The messages.</value>
        public List<RawShaderAnalysisMessage> Messages { get; private set; }
    }
}
