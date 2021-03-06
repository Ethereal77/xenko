// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;

using Stride.Core;
using Stride.Core.Mathematics;

namespace Stride.Navigation
{
    /// <summary>
    /// Represents cached data for a static collider component on an entity
    /// </summary>
    [DataContract]
    internal class NavigationMeshCachedObject
    {
        /// <summary>
        /// Guid of the collider
        /// </summary>
        public Guid Guid;

        /// <summary>
        /// Hash obtained with <see cref="NavigationMeshBuildUtils.HashEntityCollider"/>
        /// </summary>
        public int ParameterHash;

        /// <summary>
        /// Cached vertex data
        /// </summary>
        public NavigationMeshInputBuilder InputBuilder;

        /// <summary>
        /// List of infinite planes contained on this object
        /// </summary>
        public List<Plane> Planes = new List<Plane>();
    }
}
