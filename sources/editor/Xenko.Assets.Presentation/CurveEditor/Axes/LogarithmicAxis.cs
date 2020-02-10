// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace Xenko.Assets.Presentation.CurveEditor
{
    /// <summary>
    /// Represents an axis with logarithmic scale.
    /// </summary>
    public sealed class LogarithmicAxis : AxisBase
    {
        /// <inheritdoc/>
        public override bool IsXyAxis => true;

        /// <inheritdoc/>
        protected override double PostInverseTransform(double x)
        {
            return Math.Exp(x);
        }

        /// <inheritdoc/>
        protected override double PreTransform(double x)
        {
            return Math.Log(x);
        }
    }
}
