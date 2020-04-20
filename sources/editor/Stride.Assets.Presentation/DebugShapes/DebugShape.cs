// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

using Stride.Graphics;
using Stride.Rendering;

namespace Stride.Assets.Presentation.DebugShapes
{
    public class DebugShape : IDisposable
    {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
        }

        public virtual MeshDraw CreateDebugPrimitive(GraphicsDevice device)
        {
            return null;
        }
    }
}
