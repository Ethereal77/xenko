// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core;
using Stride.Core.Mathematics;

namespace Stride.Rendering
{
    public partial class SpriteBaseKeys
    {
        static SpriteBaseKeys()
        {
            MatrixTransform = ParameterKeys.NewValue(Matrix.Identity);
        }

        public static readonly PermutationParameterKey<bool> ColorIsSRgb = ParameterKeys.NewPermutation(false);
    }
}
