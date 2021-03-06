// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core;

namespace Stride.Animations
{
    /// <summary>
    /// Describes how a curve should be interpolated.
    /// </summary>
    [DataContract]
    public enum AnimationCurveInterpolationType
    {
        /// <summary>
        /// Interpolates by using constant value between keyframes.
        /// </summary>
        Constant,

        /// <summary>
        /// Interpolates linearly between keyframes.
        /// </summary>
        Linear,

        /// <summary>
        /// Interpolates with implicit derivatives using points before and after.
        /// More information at http://en.wikipedia.org/wiki/Cubic_Hermite_spline#Interpolation_on_the_unit_interval_without_exact_derivatives.
        /// </summary>
        Cubic,
    }
}
