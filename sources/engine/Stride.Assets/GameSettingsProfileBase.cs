// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.Generic;

using Stride.Core;
using Stride.Graphics;

namespace Stride.Assets
{
    /// <summary>
    /// Default game settings profile. This is currently used internally only.
    /// </summary>
    [DataContract()]
    public abstract class GameSettingsProfileBase : IGameSettingsProfile
    {
        [DataMember(10)]
        public GraphicsPlatform GraphicsPlatform { get; set; }

        public abstract IEnumerable<GraphicsPlatform> GetSupportedGraphicsPlatforms();
    }
}
