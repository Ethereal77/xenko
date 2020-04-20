// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Stride.Core.IO;

namespace Stride.Assets
{
    static class ToolLocator
    {
        public static UFile LocateTool(string toolName)
        {
            var tool = UPath.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), new UFile(toolName));
            if (File.Exists(tool))
                return tool;

            tool = UPath.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), new UFile($"../../content/{toolName}"));
            if (File.Exists(tool))
                return tool;

            return null;
        }
    }
}
