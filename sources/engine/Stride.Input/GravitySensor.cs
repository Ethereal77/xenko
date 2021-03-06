// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core.Mathematics;

namespace Stride.Input
{
    internal class GravitySensor : Sensor, IGravitySensor
    {
        public Vector3 Vector { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GravitySensor"/> class.
        /// </summary>
        public GravitySensor(IInputSource source, string systemName) : base(source, systemName, "Gravity")
        {
        }
    }
}