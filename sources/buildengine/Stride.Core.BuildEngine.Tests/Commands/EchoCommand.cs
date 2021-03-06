// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Stride.Core.Serialization.Contents;

namespace Stride.Core.BuildEngine.Tests.Commands
{
    public class EchoCommand : TestCommand
    {
        public string InputUrl { get; set; }
        public string Echo { get; set; }

        public EchoCommand(string inputUrl, string echo)
        {
            InputUrl = inputUrl;
            Echo = echo;
        }

        public override IEnumerable<ObjectUrl> GetInputFiles()
        {
            yield return new ObjectUrl(UrlType.File, InputUrl);
        }

        protected override Task<ResultStatus> DoCommandOverride(ICommandContext commandContext)
        {
            Console.WriteLine(@"{0}: {1}", InputUrl, Echo);
            return Task.FromResult(ResultStatus.Successful);
        }
    }
}
