// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core.Mathematics;
using Stride.Graphics;
using Stride.Rendering;

namespace Stride.Particles.Rendering
{
    /// <summary>
    /// Defines a particle emitter to render.
    /// </summary>
    public class RenderParticleEmitter : RenderObject
    {
        public RenderParticleSystem RenderParticleSystem;

        public ParticleEmitter ParticleEmitter;
        internal ParticleEmitterRenderFeature.ParticleMaterialInfo ParticleMaterialInfo;

        public Color4 Color;

        public bool HasVertexBufferChanged;
        public int VertexSize;
        public int VertexCount;
    }
}
