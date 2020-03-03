// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

using Xenko.Core;
using Xenko.Core.Mathematics;
using Xenko.Animations;
using Xenko.Particles.Sorters;
using Xenko.Particles.VertexLayouts;

namespace Xenko.Particles.ShapeBuilders
{
    /// <summary>
    /// Shape builder which builds each particle as a up-facing quad, further rotated by the particle's rotation in 3d space
    /// </summary>
    [DataContract("ShapeBuilderQuad")]
    [Display("Quad")]
    public class ShapeBuilderQuad : ShapeBuilderCommon
    {
        /// <inheritdoc />
        public override int QuadsPerParticle { get; protected set; } = 1;

        /// <summary>
        /// Additive animation for the particle rotation. If present, particle's own rotation will be added to the sampled curve value
        /// </summary>
        /// <userdoc>
        /// Additive animation for the particle rotation. If present, particle's own rotation will be added to the sampled curve value
        /// </userdoc>
        [DataMember(300)]
        [Display("Additive Rotation Animation")]
        public ComputeCurveSampler<Quaternion> SamplerRotation { get; set; }

        /// <inheritdoc />
        public override void PreUpdate()
        {
            base.PreUpdate();

            SamplerRotation?.UpdateChanges();
        }

        /// <inheritdoc />
        public override unsafe int BuildVertexBuffer(ref ParticleBufferState bufferState, Vector3 invViewX, Vector3 invViewY,
            ref Vector3 spaceTranslation, ref Quaternion spaceRotation, float spaceScale, ref ParticleList sorter, ref Matrix viewProj)
        {
            // Update the curve samplers if required
            base.BuildVertexBuffer(ref bufferState, invViewX, invViewY, ref spaceTranslation, ref spaceRotation, spaceScale, ref sorter, ref viewProj);

            // Get all required particle fields
            var positionField = sorter.GetField(ParticleFields.Position);
            if (!positionField.IsValid())
                return 0;
            var lifeField = sorter.GetField(ParticleFields.Life);
            var sizeField = sorter.GetField(ParticleFields.Size);
            var rotField = sorter.GetField(ParticleFields.Quaternion);
            var hasRotation = rotField.IsValid() || (SamplerRotation != null);


            // Check if the draw space is identity - in this case we don't need to transform the position, scale and rotation vectors
            var trsIdentity = (spaceScale == 1f);
            trsIdentity = trsIdentity && (spaceTranslation.Equals(Vector3.Zero));
            trsIdentity = trsIdentity && (spaceRotation.Equals(Quaternion.Identity));


            var renderedParticles = 0;

            var posAttribute = bufferState.GetAccessor(VertexAttributes.Position);
            var texAttribute = bufferState.GetAccessor(bufferState.DefaultTexCoords);

            foreach (var particle in sorter)
            {
                var centralPos = GetParticlePosition(particle, positionField, lifeField);

                var particleSize = GetParticleSize(particle, sizeField, lifeField);

                var unitX = new Vector3(1, 0, 0); 
                var unitY = new Vector3(0, 0, 1); 

                if (hasRotation)
                {
                    var particleRotation = GetParticleRotation(particle, rotField, lifeField);
                    particleRotation.Rotate(ref unitX);
                    particleRotation.Rotate(ref unitY);
                }

                // The TRS matrix is not an identity, so we need to transform the quad
                if (!trsIdentity)
                {
                    spaceRotation.Rotate(ref centralPos);
                    centralPos = centralPos * spaceScale + spaceTranslation;
                    particleSize *= spaceScale;

                    spaceRotation.Rotate(ref unitX);
                    spaceRotation.Rotate(ref unitY);
                }

                // Use half size to make a Size = 1 result in a Billboard of 1m x 1m
                unitX *= (particleSize * 0.5f);
                unitY *= (particleSize * 0.5f);

                var particlePos = centralPos - unitX + unitY;
                var uvCoord = Vector2.Zero;
                // 0f 0f
                bufferState.SetAttribute(posAttribute, (IntPtr)(&particlePos));
                bufferState.SetAttribute(texAttribute, (IntPtr)(&uvCoord));
                bufferState.NextVertex();


                // 1f 0f
                particlePos += unitX * 2;
                uvCoord.X = 1;
                bufferState.SetAttribute(posAttribute, (IntPtr)(&particlePos));
                bufferState.SetAttribute(texAttribute, (IntPtr)(&uvCoord));
                bufferState.NextVertex();


                // 1f 1f
                particlePos -= unitY * 2;
                uvCoord.Y = 1;
                bufferState.SetAttribute(posAttribute, (IntPtr)(&particlePos));
                bufferState.SetAttribute(texAttribute, (IntPtr)(&uvCoord));
                bufferState.NextVertex();


                // 0f 1f
                particlePos -= unitX * 2;
                uvCoord.X = 0;
                bufferState.SetAttribute(posAttribute, (IntPtr)(&particlePos));
                bufferState.SetAttribute(texAttribute, (IntPtr)(&uvCoord));
                bufferState.NextVertex();

                renderedParticles++;
            }

            var vtxPerShape = 4 * QuadsPerParticle;
            return renderedParticles * vtxPerShape;
        }

        /// <summary>
        /// Gets the combined rotation for the particle, adding its field value (if any) to its sampled value from the curve
        /// </summary>
        /// <param name="particle">Target particle</param>
        /// <param name="rotationField">Rotation field accessor</param>
        /// <param name="lifeField">Normalized particle life for sampling</param>
        /// <returns>Quaternion rotation of the quad particle, assuming flat horizontal square at neutral rotation</returns>
        protected unsafe Quaternion GetParticleRotation(Particle particle, ParticleFieldAccessor<Quaternion> rotationField, ParticleFieldAccessor<float> lifeField)
        {
            var particleRotation = rotationField.IsValid() ? particle.Get(rotationField) : Quaternion.Identity;

            if (SamplerRotation == null)
                return particleRotation;

            var life = 1f - (*((float*)particle[lifeField]));   // The Life field contains remaining life, so for sampling we take (1 - life)

            return SamplerRotation.Evaluate(life) * particleRotation;
        }
    }
}
