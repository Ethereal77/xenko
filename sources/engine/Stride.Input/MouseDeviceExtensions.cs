// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

namespace Stride.Input
{
    public static class MouseDeviceExtensions
    {
        /// <summary>
        /// Determines whether the specified mouse button is pressed since the previous update.
        /// </summary>
        /// <param name="mouseDevice">The mnouse</param>
        /// <param name="mouseButton">The mouse button</param>
        /// <returns><c>true</c> if the specified mouse button is pressed since the previous update; otherwise, <c>false</c>.</returns>
        public static bool IsButtonPressed(this IMouseDevice mouseDevice, MouseButton mouseButton)
        {
            return mouseDevice.PressedButtons.Contains(mouseButton);
        }

        /// <summary>
        /// Determines whether the specified mouse button is released.
        /// </summary>
        /// <param name="mouseDevice">The mnouse</param>
        /// <param name="mouseButton">The mouse button</param>
        /// <returns><c>true</c> if the specified mouse button is released; otherwise, <c>false</c>.</returns>
        public static bool IsButtonReleased(this IMouseDevice mouseDevice, MouseButton mouseButton)
        {
            return mouseDevice.ReleasedButtons.Contains(mouseButton);
        }

        /// <summary>
        /// Determines whether the specified button is being pressed down
        /// </summary>
        /// <param name="mouseDevice">The mnouse</param>
        /// <param name="mouseButton">The mouse button</param>
        /// <returns><c>true</c> if the specified button is being pressed down; otherwise, <c>false</c>.</returns>
        public static bool IsButtonDown(this IMouseDevice mouseDevice, MouseButton mouseButton)
        {
            return mouseDevice.DownButtons.Contains(mouseButton);
        }
    }
}
