// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core;

namespace Xenko.Graphics
{
    /// <summary>
    /// A geometric data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GeometricMeshData<T> : ComponentBase where T : struct, IVertex
    {
        public GeometricMeshData(T[] vertices, int[] indices, bool isLeftHanded)
        {
            Vertices = vertices;
            Indices = indices;
            IsLeftHanded = isLeftHanded;
        }

        /// <summary>
        /// Gets or sets the vertices.
        /// </summary>
        /// <value>The vertices.</value>
        public T[] Vertices { get; set; }

        /// <summary>
        /// Gets or sets the indices.
        /// </summary>
        /// <value>The indices.</value>
        public int[] Indices { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is left handed.
        /// </summary>
        /// <value><c>true</c> if this instance is left handed; otherwise, <c>false</c>.</value>
        public bool IsLeftHanded { get; set; }
    }
}
