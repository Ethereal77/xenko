// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Core.Serialization;

namespace Stride.Core.Assets.Serializers
{
    /// <summary>
    /// A fake serializer used for cloning invariant objects. 
    /// Instead of actually cloning the invariant object, this serializer store it in a list of the context and restore when deserializing.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataSerializerGlobal(typeof(InvariantObjectCloneSerializer<string>), Profile = "AssetClone")]
    public class InvariantObjectCloneSerializer<T> : DataSerializer<T>
    {
        public override void Serialize(ref T obj, ArchiveMode mode, SerializationStream stream)
        {
            var invariantObjectList = stream.Context.Get(AssetCloner.InvariantObjectListProperty);
            if (mode == ArchiveMode.Serialize)
            {
                stream.Write(invariantObjectList.Count);
                invariantObjectList.Add(obj);
            }
            else
            {
                var index = stream.Read<Int32>();
                obj = (T)invariantObjectList[index];
            }
        }
    }
}
