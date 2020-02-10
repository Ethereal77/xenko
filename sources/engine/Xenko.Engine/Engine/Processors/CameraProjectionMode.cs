// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core;

namespace Xenko.Engine.Processors
{
    /// <summary>
    /// Projection of a <see cref="CameraComponent"/>.
    /// </summary>
    [DataContract("CameraProjectionMode")]
    public enum CameraProjectionMode
    {
        /// <summary>
        /// A perspective projection.
        /// </summary>
        /// <userdoc>A perspective projection (usually used for 3D games).</userdoc>
        Perspective,

        /// <summary>
        /// An orthographic projection.
        /// </summary>
        /// <userdoc>An orthographic projection (usually used for 2D games).</userdoc>
        Orthographic,
    }
}
