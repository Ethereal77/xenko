// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.ObjectModel;

using Stride.Core;

namespace Stride.Core.Assets
{
    /// <summary>
    /// A collection of <see cref="SolutionPlatform"/>.
    /// </summary>
    public sealed class SolutionPlatformCollection : KeyedCollection<PlatformType, SolutionPlatform>
    {
        protected override PlatformType GetKeyForItem(SolutionPlatform item)
        {
            return item.Type;
        }
    }
}
