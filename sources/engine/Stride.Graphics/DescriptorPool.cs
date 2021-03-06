// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

namespace Stride.Graphics
{
    /// <summary>
    /// Storage area for <see cref="DescriptorSet"/>.
    /// </summary>
    public partial class DescriptorPool : GraphicsResourceBase
    {
        public static DescriptorPool New(GraphicsDevice graphicsDevice, DescriptorTypeCount[] counts)
        {
            return new DescriptorPool(graphicsDevice, counts);
        }

#if STRIDE_GRAPHICS_API_DIRECT3D11
        internal DescriptorSetEntry[] Entries;
        private int descriptorAllocationOffset;

        private DescriptorPool(GraphicsDevice graphicsDevice, DescriptorTypeCount[] counts)
        {
            // For now, we put everything together so let's compute total count
            var totalCount = 0;
            foreach (var count in counts)
            {
                totalCount += count.Count;
            }

            Entries = new DescriptorSetEntry[totalCount];
        }

        protected override void Destroy()
        {
            Entries = null;
            base.Destroy();
        }

        public void Reset()
        {
            Array.Clear(Entries, 0, descriptorAllocationOffset);
            descriptorAllocationOffset = 0;
        }

        internal int Allocate(int size)
        {
            if (descriptorAllocationOffset + size > Entries.Length)
                return -1;

            var result = descriptorAllocationOffset;
            descriptorAllocationOffset += size;
            return result;
        }
#endif
    }
}
