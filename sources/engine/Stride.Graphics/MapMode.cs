// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

namespace Stride.Graphics
{
    /// <summary>
    /// Describes how the cpu is accessing a <see cref="GraphicsResource"/> with the <see cref="GraphicsDeviceContext.Map"/> method.
    /// </summary>
    public enum MapMode
    {
        /// <summary>
        /// Resource is mapped for reading. 
        /// </summary>
        /// <remarks>
        /// The resource must have been created with usage <see cref="GraphicsResourceUsage.Staging"/>.
        /// </remarks>
        Read = 1,

        /// <summary>
        /// Resource is mapped for writing. 
        /// </summary>
        /// <remarks>
        /// The resource must have been created with usage <see cref="GraphicsResourceUsage.Dynamic"/> or <see cref="GraphicsResourceUsage.Staging"/>.
        /// </remarks>
        Write = 2,

        /// <summary>
        /// Resource is mapped for read-write.
        /// </summary>
        /// <remarks>
        /// The resource must have been created with usage <see cref="GraphicsResourceUsage.Staging"/>.
        /// </remarks>
        ReadWrite = 3,

        /// <summary>
        /// Resource is mapped for writing; the previous contents of the resource will be undefined.
        /// </summary>
        /// <remarks>
        /// The resource must have been created with usage <see cref="GraphicsResourceUsage.Dynamic"/>.
        /// </remarks>
        WriteDiscard = 4,

        /// <summary>
        /// Resource is mapped for writing; the existing contents of the resource cannot be overwritten.
        /// </summary>
        /// <remarks>
        /// This flag is only valid on vertex and index buffers.
        /// </remarks>
        WriteNoOverwrite = 5,
    }
}
