// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Stride.Core.Assets.Editor
{
    public static class EditorPath
    {
        public static string EditorTitle { get; set; }

        public static string UserDataPath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            ProductNameDirectory);

        public static string DefaultTempPath => Path.Combine(
            Path.GetTempPath(),
            ProductNameDirectory);

        public static string EditorConfigPath => Path.Combine(UserDataPath, "GameStudioSettings.conf");

        public static string InternalConfigPath => Path.Combine(UserDataPath, "GameStudioInternal.conf");
 
        private static string ProductNameDirectory => "Stride";
    }
}
