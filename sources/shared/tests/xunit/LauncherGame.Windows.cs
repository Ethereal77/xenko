// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xunit.runner.stride
{
    class Program
    {
        public static void Main(string[] args) => StrideXunitRunner.Main(args, interactiveMode => GameTestBase.ForceInteractiveMode = interactiveMode);
    }
}
