// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.Generic;

using Xenko.Graphics;

namespace Xenko.Assets
{
    /// <summary>
    /// Base interface for game settings for a particular profile
    /// </summary>
    public interface IGameSettingsProfile
    {
        /// <summary>
        /// Gets the GraphicsPlatform used by this profile.
        /// </summary>
        GraphicsPlatform GraphicsPlatform { get; }

        /// <summary>
        /// Gets the <see cref="GraphicsPlatform"/> list supported by this profile.
        /// </summary>
        /// <returns></returns>
        IEnumerable<GraphicsPlatform> GetSupportedGraphicsPlatforms();
    }
}
