// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Rendering;
using Xenko.Shaders;

namespace Xenko.Graphics
{
    public static class ParameterCollectionLayoutExtensions
    {
        public static void ProcessResources(this ParameterCollectionLayout parameterCollectionLayout, DescriptorSetLayoutBuilder layout)
        {
            foreach (var layoutEntry in layout.Entries)
            {
                parameterCollectionLayout.LayoutParameterKeyInfos.Add(new ParameterKeyInfo(layoutEntry.Key, parameterCollectionLayout.ResourceCount++));
            }
        }

        public static void ProcessConstantBuffer(this ParameterCollectionLayout parameterCollectionLayout, EffectConstantBufferDescription constantBuffer)
        {
            foreach (var member in constantBuffer.Members)
            {
                parameterCollectionLayout.LayoutParameterKeyInfos.Add(new ParameterKeyInfo(member.KeyInfo.Key, parameterCollectionLayout.BufferSize + member.Offset, member.Type.Elements > 0 ? member.Type.Elements : 1));
            }
            parameterCollectionLayout.BufferSize += constantBuffer.Size;
        }
    }
}
