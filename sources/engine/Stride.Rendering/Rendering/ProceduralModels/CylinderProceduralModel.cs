// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.ComponentModel;

using Stride.Core;
using Stride.Graphics;
using Stride.Graphics.GeometricPrimitives;

namespace Stride.Rendering.ProceduralModels
{
    /// <summary>
    /// A Cylinder descriptor
    /// </summary>
    [DataContract("CylinderProceduralModel")]
    [Display("Cylinder")]
    public class CylinderProceduralModel : PrimitiveProceduralModelBase
    {
        /// <summary>
        /// Initializes a new instance of the Cylinder descriptor class.
        /// </summary>
        public CylinderProceduralModel()
        {
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        /// <userdoc>The height of the cylinder.</userdoc>
        [DataMember(10)]
        [DefaultValue(1.0f)]
        public float Height { get; set; } = 1.0f;

        /// <summary>
        /// Gets or sets the radius of the base of the cylinder.
        /// </summary>
        /// <value>The radius.</value>
        /// <userdoc>The radius of the cylinder.</userdoc>
        [DataMember(20)]
        [DefaultValue(0.5f)]
        public float Radius { get; set; } = 0.5f;

        /// <summary>
        /// Gets or sets the tessellation factor.
        /// </summary>
        /// <value>The tessellation.</value>
        /// <userdoc>The tessellation of the cylinder. That is the number of polygons composing it.</userdoc>
        [DataMember(30)]
        [DefaultValue(32)]
        public int Tessellation { get; set; } = 32;

        protected override GeometricMeshData<VertexPositionNormalTexture> CreatePrimitiveMeshData()
        {
            return GeometricPrimitive.Cylinder.New(Height, Radius, Tessellation, UvScale.X, UvScale.Y);
        }
    }
}
