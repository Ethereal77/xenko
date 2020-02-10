// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Linq;

using Mono.Cecil.Rocks;

namespace Xenko.Core.AssemblyProcessor
{
    internal class InitLocalsProcessor : IAssemblyDefinitionProcessor
    {
        public bool Process(AssemblyProcessorContext context)
        {
            bool changed = false;
            foreach (var type in context.Assembly.MainModule.GetAllTypes())
            {
                foreach (var method in type.Methods)
                {
                    if (method.CustomAttributes.Any(x => x.AttributeType.FullName == "Xenko.Core.IL.RemoveInitLocalsAttribute"))
                    {
                        if (method.Body == null)
                        {
                            throw new InvalidOperationException($"Trying to remove initlocals from method {method.FullName} without body.");
                        }

                        method.Body.InitLocals = false;
                        changed = true;
                    }
                }
            }

            return changed;
        }
    }
}
