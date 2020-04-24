﻿// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Shaders;

namespace Stride.Rendering.Voxels
{
    public partial class MarchAttributesKeys
    {
        public static readonly PermutationParameterKey<ShaderSourceCollection> AttributeSamplers = ParameterKeys.NewPermutation<ShaderSourceCollection>();
    }
}
