// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Core.Mathematics;

namespace Stride.Input
{
    /// <summary>
    /// This class represents a sensor of type Orientation. It measures the orientation of device in the real world.
    /// </summary>
    public interface IOrientationSensor : ISensorDevice
    {
        /// <summary>
        /// Gets the value of the yaw (in radian). The yaw is the rotation around the vertical axis of the device, that is the Oz axis.
        /// </summary>
        float Yaw { get; }

        /// <summary>
        /// Gets the value of the pitch (in radian). The pitch is the rotation around the lateral axis of the device, that is the Ox axis.
        /// </summary>
        float Pitch { get; }

        /// <summary>
        /// Gets the value of the roll (in radian). The roll is the rotation around the longitudinal axis of the device, that is the Oy axis.
        /// </summary>
        float Roll { get; }

        /// <summary>
        /// Gets the quaternion specifying the current rotation of the device.
        /// </summary>
        Quaternion Quaternion { get; }

        /// <summary>
        /// Gets the rotation matrix specifying the current rotation of the device.
        /// </summary>
        Matrix RotationMatrix { get;  }
    }
}
