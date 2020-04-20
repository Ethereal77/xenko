// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

#if STRIDE_GRAPHICS_API_NULL 

using System;

namespace Stride.Graphics
{
    public partial class SamplerState
    {
        private SamplerState(GraphicsDevice graphicsDevice, SamplerStateDescription samplerStateDescription)
        {
            throw new NotImplementedException();
        }
    }
} 
#endif 
