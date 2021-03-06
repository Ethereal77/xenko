// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core;

namespace Stride.Rendering.Images
{
    /// <summary>
    /// The tonemap Reinhard operator.
    /// </summary>
    [DataContract("ToneMapReinhardOperator")]
    [Display("Reinhard")]
    public class ToneMapReinhardOperator : ToneMapCommonOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToneMapReinhardOperator"/> class.
        /// </summary>
        public ToneMapReinhardOperator()
            : base("ToneMapReinhardOperatorShader")
        {
        }
    }
}
