// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Graphics;

namespace Xenko.Games
{
    /// <summary>
    /// Defines the interface for an object that manages a GraphicsDevice.
    /// </summary>
    public interface IGraphicsDeviceManager
    {
        /// <summary>
        /// Starts the drawing of a frame.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        bool BeginDraw();

        /// <summary>
        /// Called to ensure that the device manager has created a valid device.
        /// </summary>
        void CreateDevice();

        /// <summary>
        /// Called by the game at the end of drawing; if requested, presents the final rendering.
        /// </summary>
        void EndDraw(bool present);
    }
}
