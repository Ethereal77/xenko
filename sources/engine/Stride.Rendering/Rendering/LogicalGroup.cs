// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core.Storage;

namespace Stride.Rendering
{
    /// <summary>
    /// Defines a group of descriptors and cbuffer range that are updated together.
    /// It can be declared in shader using the syntax <c>cbuffer PerView_LogicalGroupName</c> (also works with <c>rgroup</c>).
    /// </summary>
    public struct LogicalGroup
    {
        public ObjectId Hash;

        public int DescriptorEntryStart;
        public int DescriptorEntryCount;
        public int DescriptorSlotStart;
        public int DescriptorSlotCount;

        public int ConstantBufferMemberStart;
        public int ConstantBufferMemberCount;
        public int ConstantBufferOffset;
        public int ConstantBufferSize;
    }
}
