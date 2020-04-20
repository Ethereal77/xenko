// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.Generic;

using Stride.Core;

namespace Stride.Assets.Presentation.NodePresenters.Keys
{
    public static class SpriteFontData
    {
        public const string SpriteFontAvailableFonts = nameof(SpriteFontAvailableFonts);
        public static readonly PropertyKey<IEnumerable<string>> Key = new PropertyKey<IEnumerable<string>>(SpriteFontAvailableFonts, typeof(SpriteFontData));
    }
}
