// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Stride.Assets.Presentation.CurveEditor
{
    /// <summary>
    /// Represents an axis with linear scale.
    /// </summary>
    public class LinearAxis : AxisBase
    {
        /// <inheritdoc/>
        public override bool IsXyAxis => true;

        /// <inheritdoc/>
        protected override double PostInverseTransform(double x)
        {
            return x;
        }

        /// <inheritdoc/>
        protected override double PreTransform(double x)
        {
            return x;
        }
    }
}
