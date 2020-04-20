// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Stride.Rendering.Images
{
    /// <summary>
    /// Class LuminanceLogEffect.
    /// </summary>
    public class LuminanceLogEffect : ImageEffectShader
    {
        public LuminanceLogEffect(string luminanceShaderName = "LuminanceLogShader")
        {
            EffectName = luminanceShaderName;
        }
    }
}
