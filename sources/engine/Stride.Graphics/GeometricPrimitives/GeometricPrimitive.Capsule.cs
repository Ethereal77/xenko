// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

// -----------------------------------------------------------------------------
// The following code is a port of DirectXTk http://directxtk.codeplex.com
// Licensed under Microsoft Public License (Ms-PL)
// -----------------------------------------------------------------------------

using System;

using Stride.Core.Mathematics;

namespace Stride.Graphics.GeometricPrimitives
{
    public partial class GeometricPrimitive
    {
        /// <summary>
        /// A sphere primitive.
        /// </summary>
        public static class Capsule
        {
            /// <summary>
            /// Creates a sphere primitive.
            /// </summary>
            /// <param name="device">The device.</param>
            /// <param name="length">The length. That is the distance between the two sphere centers.</param>
            /// <param name="radius">The radius of the capsule.</param>
            /// <param name="tessellation">The tessellation.</param>
            /// <param name="uScale">Scale U coordinates between 0 and the values of this parameter.</param>
            /// <param name="vScale">Scale V coordinates 0 and the values of this parameter.</param>
            /// <param name="toLeftHanded">if set to <c>true</c> vertices and indices will be transformed to left handed. Default is false.</param>
            /// <returns>A sphere primitive.</returns>
            /// <exception cref="System.ArgumentOutOfRangeException">tessellation;Must be &gt;= 3</exception>
            public static GeometricPrimitive New(GraphicsDevice device, float length = 1.0f, float radius = 0.5f, int tessellation = 8, float uScale = 1.0f, float vScale = 1.0f, bool toLeftHanded = false)
            {
                return new GeometricPrimitive(device, New(length, radius, tessellation, uScale, vScale, toLeftHanded));
            }

            /// <summary>
            /// Creates a sphere primitive.
            /// </summary>
            /// <param name="length">The length of the capsule. That is the distance between the two sphere centers.</param>
            /// <param name="radius">The radius of the capsule.</param>
            /// <param name="tessellation">The tessellation.</param>
            /// <param name="uScale">Scale U coordinates between 0 and the values of this parameter.</param>
            /// <param name="vScale">Scale V coordinates 0 and the values of this parameter.</param>
            /// <param name="toLeftHanded">if set to <c>true</c> vertices and indices will be transformed to left handed. Default is false.</param>
            /// <returns>A sphere primitive.</returns>
            /// <exception cref="System.ArgumentOutOfRangeException">tessellation;Must be &gt;= 3</exception>
            public static GeometricMeshData<VertexPositionNormalTexture> New(float length = 1.0f, float radius = 0.5f, int tessellation = 8, float uScale = 1.0f, float vScale = 1.0f, bool toLeftHanded = false)
            {
                if (tessellation < 3) tessellation = 3;

                int verticalSegments = 2 * tessellation;
                int horizontalSegments = 4 * tessellation;

                var vertices = new VertexPositionNormalTexture[verticalSegments * (horizontalSegments + 1)];
                var indices = new int[(verticalSegments - 1) * (horizontalSegments + 1) * 6];

                var vertexCount = 0;
                // Create rings of vertices at progressively higher latitudes.
                for (int i = 0; i < verticalSegments; i++)
                {
                    float v;
                    float deltaY;
                    float latitude;
                    if (i < verticalSegments / 2)
                    {
                        deltaY = -length / 2;
                        v = 1.0f - (0.25f * i / (tessellation - 1));
                        latitude = (float)((i * Math.PI / (verticalSegments - 2)) - Math.PI / 2.0);
                    }
                    else
                    {
                        deltaY = length / 2;
                        v = 0.5f - (0.25f * (i - 1) / (tessellation - 1));
                        latitude = (float)(((i - 1) * Math.PI / (verticalSegments - 2)) - Math.PI / 2.0);
                    }

                    var dy = (float)Math.Sin(latitude);
                    var dxz = (float)Math.Cos(latitude);

                    // Create a single ring of vertices at this latitude.
                    for (int j = 0; j <= horizontalSegments; j++)
                    {
                        float u = (float)j / horizontalSegments;

                        var longitude = (float)(j * 2.0 * Math.PI / horizontalSegments);
                        var dx = (float)Math.Sin(longitude);
                        var dz = (float)Math.Cos(longitude);

                        dx *= dxz;
                        dz *= dxz;

                        var normal = new Vector3(dx, dy, dz);
                        var textureCoordinate = new Vector2(u * uScale, v * vScale);
                        var position = radius * normal + new Vector3(0, deltaY, 0);

                        vertices[vertexCount++] = new VertexPositionNormalTexture(position, normal, textureCoordinate);
                    }
                }

                // Fill the index buffer with triangles joining each pair of latitude rings.
                int stride = horizontalSegments + 1;

                int indexCount = 0;
                for (int i = 0; i < verticalSegments - 1; i++)
                {
                    for (int j = 0; j <= horizontalSegments; j++)
                    {
                        int nextI = i + 1;
                        int nextJ = (j + 1) % stride;

                        indices[indexCount++] = (i * stride + j);
                        indices[indexCount++] = (nextI * stride + j);
                        indices[indexCount++] = (i * stride + nextJ);

                        indices[indexCount++] = (i * stride + nextJ);
                        indices[indexCount++] = (nextI * stride + j);
                        indices[indexCount++] = (nextI * stride + nextJ);
                    }
                }

                // Create the primitive object.
                // Create the primitive object.
                return new GeometricMeshData<VertexPositionNormalTexture>(vertices, indices, toLeftHanded) { Name = "Capsule" };
            }
        }
    }
}
