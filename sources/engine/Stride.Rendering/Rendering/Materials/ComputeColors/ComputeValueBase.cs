// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core;
using Stride.Core.Annotations;

namespace Stride.Rendering.Materials.ComputeColors
{
    /// <summary>
    /// Base class for a constant value for <see cref="ComputeNode"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract(Inherited = true)]
    public abstract class ComputeValueBase<T> : ComputeKeyedBase
    {
        private T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputeValueBase{T}"/> class.
        /// </summary>
        protected ComputeValueBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputeValueBase{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        protected ComputeValueBase(T value) : this()
        {
            Value = value;
        }

        /// <summary>
        /// The property to access the internal value
        /// </summary>
        /// <userdoc>
        /// The value.
        /// </userdoc>
        [DataMember(20)]
        [InlineProperty]
        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                if (!value.Equals(this.value))
                {
                    this.value = value;
                }
            }
        }
    }
}
