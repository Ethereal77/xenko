// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Core;
using Stride.Core.Mathematics;

namespace Stride.Physics
{
    [DataContract]
    [Display("Float")]
    public class FloatHeightStickArraySource : IHeightStickArraySource
    {
        [DataMemberIgnore]
        public HeightfieldTypes HeightType => HeightfieldTypes.Float;

        [DataMember(10)]
        [Display("Size")]
        public Int2 HeightStickSize { get; set; } = new Int2(65, 65);

        [DataMember(20)]
        public Vector2 HeightRange { get; set; } = new Vector2(-10, 10);

        [DataMemberIgnore]
        public float HeightScale => 1f;

        /// <summary>
        ///   The value to fill the height stick array.
        /// </summary>
        [DataMember(30)]
        public float InitialHeight { get; set; } = 0;

        public bool IsValid() =>
            HeightmapUtils.CheckHeightParameters(HeightStickSize, HeightType, HeightRange, HeightScale, false) &&
            MathUtil.IsInRange(InitialHeight, HeightRange.X, HeightRange.Y);

        public void CopyTo<T>(UnmanagedArray<T> heightStickArray, int index) where T : struct
        {
            if (heightStickArray is null)
                throw new ArgumentNullException(nameof(heightStickArray));

            if (heightStickArray is UnmanagedArray<float> unmanagedArray)
            {
                unmanagedArray.Fill(InitialHeight, index, HeightStickSize.X * HeightStickSize.Y);
            }
            else
                throw new NotSupportedException($"{ typeof(UnmanagedArray<T>) } type is not supported.");
        }

        public bool Match(object obj)
        {
            var other = obj as FloatHeightStickArraySource;

            if (other is null)
                return false;

            return other.HeightStickSize == HeightStickSize &&
                   other.HeightRange == HeightRange &&
                   Math.Abs(other.InitialHeight - InitialHeight) < float.Epsilon;
        }
    }
}
