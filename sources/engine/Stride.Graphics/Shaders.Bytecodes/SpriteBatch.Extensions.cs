// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Shaders;

namespace Stride.Graphics
{
    public partial class SpriteBatch
    {
        private static EffectBytecode bytecode = null;
        private static EffectBytecode bytecodeSRgb = null;

        internal static EffectBytecode Bytecode
        {
            get
            {
                return bytecode ?? (bytecode = EffectBytecode.FromBytesSafe(binaryBytecode));
            }
        }

        internal static EffectBytecode BytecodeSRgb
        {
            get
            {
                return bytecodeSRgb ?? (bytecodeSRgb = EffectBytecode.FromBytesSafe(binaryBytecodeSRgb));
            }
        }
    }
}
