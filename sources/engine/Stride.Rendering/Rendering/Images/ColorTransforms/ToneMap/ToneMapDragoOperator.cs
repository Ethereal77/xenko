// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.ComponentModel;

using Stride.Core;

namespace Stride.Rendering.Images
{
    /// <summary>
    /// The tonemap Drago operator.
    /// </summary>
    [DataContract("ToneMapDragoOperator")]
    [Display("Drago")]
    public class ToneMapDragoOperator : ToneMapCommonOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToneMapDragoOperator"/> class.
        /// </summary>
        public ToneMapDragoOperator()
            : base("ToneMapDragoOperatorShader")
        {
        }

        /// <summary>
        /// Gets or sets the bias.
        /// </summary>
        /// <value>The bias.</value>
        [DataMember(10)]
        [DefaultValue(0.5f)]
        public float Bias
        {
            get
            {
                return Parameters.Get(ToneMapDragoOperatorShaderKeys.DragoBias);
            }
            set
            {
                Parameters.Set(ToneMapDragoOperatorShaderKeys.DragoBias, value);
            }
        }
    }
}
