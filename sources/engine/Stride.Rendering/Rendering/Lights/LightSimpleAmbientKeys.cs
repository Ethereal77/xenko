// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;

using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Shaders;

namespace Stride.Rendering.Lights
{
    public static partial class LightSimpleAmbientKeys
    {
        static LightSimpleAmbientKeys()
        {
            AmbientLight = ParameterKeys.NewValue(new Color3(1.0f, 1.0f, 1.0f));
        }
    }
}
