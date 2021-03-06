// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Core;
using Stride.Data;

namespace Stride.Graphics
{
    //Workaround needed for now, since we don't support orientation changes during game play
    public enum RequiredDisplayOrientation
    {
        /// <summary>
        /// The default value for the orientation.
        /// </summary>
        Default = DisplayOrientation.Default,

        /// <summary>
        /// Displays in landscape mode to the left.
        /// </summary>
        [Display("Landscape Left")]
        LandscapeLeft = DisplayOrientation.LandscapeLeft,

        /// <summary>
        /// Displays in landscape mode to the right.
        /// </summary>
        [Display("Landscape Right")]
        LandscapeRight = DisplayOrientation.LandscapeRight,

        /// <summary>
        /// Displays in portrait mode.
        /// </summary>
        Portrait = DisplayOrientation.Portrait,
    }

    public enum PreferredGraphicsPlatform
    {
        Default,

        /// <summary>
        /// Direct3D11.
        /// </summary>
        Direct3D11,

        /// <summary>
        /// Direct3D12.
        /// </summary>
        Direct3D12
    }

    [DataContract]
    [Display("Rendering")]
    public class RenderingSettings : Configuration
    {
        /// <summary>
        /// Gets or sets the width of the back buffer.
        /// </summary>
        /// <userdoc>
        /// The desired back buffer width.
        /// Might be overriden depending on actual device resolution and/or ratio.
        /// On Windows, it will be the window size.
        /// </userdoc>
        [DataMember(0)]
        public int DefaultBackBufferWidth = 1280;

        /// <summary>
        /// Gets or sets the height of the back buffer.
        /// </summary>
        /// <userdoc>
        /// The desired back buffer height.
        /// Might be overriden depending on actual device resolution and/or ratio.
        /// On Windows, it will be the window size.
        /// </userdoc>
        [DataMember(10)]
        public int DefaultBackBufferHeight = 720;

        /// <summary>
        /// Gets or sets a value that if true will make sure that the aspect ratio of screen is kept.
        /// </summary>
        /// <userdoc>
        /// If true, adapt the ratio of the back buffer so that it fits the screen ratio.
        /// </userdoc>
        [DataMember(15)]
        public bool AdaptBackBufferToScreen = false;

        /// <summary>
        /// Gets or sets the default graphics profile.
        /// </summary>
        /// <userdoc>The graphics feature level this game require.</userdoc>
        [DataMember(20)]
        public GraphicsProfile DefaultGraphicsProfile = GraphicsProfile.Level_11_0;

        /// <summary>
        /// Gets or sets the colorspace.
        /// </summary>
        /// <value>The colorspace.</value>
        /// <userdoc>The colorspace (Gamma or Linear) used for rendering. This value affects both the runtime and editor.</userdoc>
        [DataMember(30)]
        public ColorSpace ColorSpace = ColorSpace.Linear;

        /// <summary>
        /// Gets or sets the display orientation.
        /// </summary>
        /// <userdoc>The display orientations this game support.</userdoc>
        [DataMember(40)]
        public RequiredDisplayOrientation DisplayOrientation = RequiredDisplayOrientation.Default;
    }
}
