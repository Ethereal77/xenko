// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core;

namespace Stride.Rendering.Materials.ComputeColors
{
    /// <summary>
    /// A node that describe a binary operation between two <see cref="IComputeColor"/>
    /// </summary>
    [DataContract("ComputeBinaryColor")]
    [Display("Binary Operator")]
    public class ComputeBinaryColor : ComputeBinaryBase<IComputeColor>, IComputeColor
    {
        private BinaryOperator cachedOperator;

        public ComputeBinaryColor()
        {
        }

        public ComputeBinaryColor(IComputeColor leftChild, IComputeColor rightChild, BinaryOperator binaryOperator)
            : base(leftChild, rightChild, binaryOperator)
        {
        }

        /// <inheritdoc/>
        public bool HasChanged
        {
            get
            {
                // Null children force skip changes
                if (LeftChild == null || RightChild == null || ((cachedOperator == Operator) && !LeftChild.HasChanged && !RightChild.HasChanged))
                    return false;

                cachedOperator = Operator;
                return true;
            }
        }
    }
}
