// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

using Xenko.Core.IO;

namespace Xenko.Core.Settings
{
    /// <summary>
    /// Arguments of the <see cref="SettingsContainer.SettingsFileLoaded"/> event.
    /// </summary>
    public class SettingsFileLoadedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsFileLoadedEventArgs"/> class.
        /// </summary>
        /// <param name="path"></param>
        public SettingsFileLoadedEventArgs(UFile path)
        {
            FilePath = path;
        }

        /// <summary>
        /// Gets the path of the file that has been loaded.
        /// </summary>
        public UFile FilePath { get; private set; }
    }
}
