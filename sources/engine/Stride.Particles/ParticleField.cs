// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

namespace Stride.Particles
{
    internal struct ParticleField
    {
#if PARTICLES_SOA
        /// <summary>
        /// Offset of the field from the particle pool's head
        /// </summary>
        public IntPtr Offset;

        /// <summary>
        /// Size of one data unit. Depends of how you group the fields together (AoS or SoA)
        /// </summary>
        public readonly int Size;

        public ParticleField(int fieldSize, IntPtr offset)
        {
            Offset = offset;
            Size = fieldSize;
        }
#else
        /// <summary>
        /// Offset of the field from the particle's position
        /// </summary>
        public int Offset;

        /// <summary>
        /// Size of the field
        /// </summary>
        public int Size;
#endif
    }
}
