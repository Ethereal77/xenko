// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core.Mathematics;

namespace Stride.Navigation
{
    /// <summary>
    /// Result for a raycast query on a navigation mesh
    /// </summary>
    public struct NavigationRaycastResult
    {
        /// <summary>
        /// true if the raycast hit something
        /// </summary>
        public bool Hit;

        /// <summary>
        /// Position where the ray hit a non-walkable area boundary
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// Normal of the non-walkable area boundary that was hit
        /// </summary>
        public Vector3 Normal;
    }
}
