// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.IO;
using System.Linq;

namespace Stride.VisualStudio.Package.Tests
{
    internal static class VSLocator
    {
        public static string VisualStudioPath
        {
            get
            {
                // We get devenv build path from devenvpath.txt -- it should have been created by the build (this happens in the .csproj file)
                var devenvPathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "devenvpath.txt");
                if (!File.Exists(devenvPathFile))
                    throw new FileNotFoundException($"devenvpath.txt was not properly generated by the build. Please check MSBuild and project files.");

                return File.ReadAllLines(devenvPathFile).First();
            }
        }
    }
}
