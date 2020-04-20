// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.Generic;
using System.Linq;

using Stride.Core.Annotations;

namespace Stride.Core.Serialization
{
    internal static class StringHashHelper
    {
        public static uint GetSerializerHashCode([NotNull] this string param)
        {
            uint result = 0;
            foreach (var c in param)
            {
                result ^= result << 4;
                result ^= result << 24;
                result ^= c;
            }
            return result;
        }
    }
}
