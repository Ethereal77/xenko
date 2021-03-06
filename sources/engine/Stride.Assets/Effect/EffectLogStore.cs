// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Text;

using Stride.Core.IO;
using Stride.Core.Yaml;
using Stride.Rendering;
using Stride.Shaders.Compiler;

namespace Stride.Assets.Effect
{
    public class EffectLogStore : DictionaryStore<EffectCompileRequest, bool>
    {
        private byte[] documentMarker = Encoding.UTF8.GetBytes("---\r\n");

        public EffectLogStore(Stream stream)
            : base(stream)
        {
        }

        protected override void WriteEntry(Stream stream, KeyValuePair<EffectCompileRequest, bool> value)
        {
            stream.Write(documentMarker, 0, documentMarker.Length);
            AssetYamlSerializer.Default.Serialize(stream, value.Key);
        }

        protected override List<KeyValuePair<EffectCompileRequest, bool>> ReadEntries(Stream localStream)
        {
            var result = new List<KeyValuePair<EffectCompileRequest, bool>>();

            foreach (var effectCompileRequest in AssetYamlSerializer.Default.DeserializeMultiple<EffectCompileRequest>(localStream))
            {
                result.Add(new KeyValuePair<EffectCompileRequest, bool>(effectCompileRequest, true));
            }

            return result;
        }
    }
}
