// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

namespace Stride.Input
{
    public static class GameControllerExtensions
    {
        /// <summary>
        /// Gets the number of buttons on this gamepad
        /// </summary>
        /// <param name="device">The gamepad</param>
        /// <returns>The number of buttons</returns>
        public static int GetButtonCount(this IGameControllerDevice device)
        {
            return device.ButtonInfos.Count;
        }

        /// <summary>
        /// Gets the number of axes on this gamepad
        /// </summary>
        /// <param name="device">The gamepad</param>
        /// <returns>The number of axes</returns>
        public static int GetAxisCount(this IGameControllerDevice device)
        {
            return device.AxisInfos.Count;
        }

        /// <summary>
        /// Gets the number of direction inputs on this gamepad
        /// </summary>
        /// <param name="device">The gamepad</param>
        /// <returns>The number of direction controllers</returns>
        public static int GetDirectionCount(this IGameControllerDevice device)
        {
            return device.DirectionInfos.Count;
        }

        /// <summary>
        /// Returns the value of a direction controller converted to a <see cref="GamePadButton"/> which has the matching Pad flags set
        /// </summary>
        /// <param name="device">The gamepad</param>
        /// <param name="index">The index of the direction controller</param>
        /// <returns></returns>
        public static GamePadButton GetDPad(this IGameControllerDevice device, int index)
        {
            var dir = device.GetDirection(index);
            return dir.IsNeutral ? GamePadButton.None : GameControllerUtils.DirectionToButtons(dir);
        }
    }
}