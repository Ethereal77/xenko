// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Xenko.Input
{
    /// <summary>
    /// An event to describe a change in a gamepad axis
    /// </summary>
    public class GamePadAxisEvent : AxisEvent
    {
        /// <summary>
        /// The gamepad axis identifier
        /// </summary>
        public GamePadAxis Axis;

        /// <summary>
        /// The gamepad that sent this event
        /// </summary>
        public IGamePadDevice GamePad => (IGamePadDevice)Device;

        public override string ToString()
        {
            return $"{nameof(Axis)}: {Axis}, {nameof(Value)}: {Value}, {nameof(GamePad)}: {GamePad.Name}";
        }
    }
}