// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Mono.Cecil;

namespace Stride.Core.AssemblyProcessor
{
    /// <summary>
    /// Generates enum serializer type from a given enum type.
    /// </summary>
    public class CecilEnumSerializerFactory : ICecilSerializerFactory
    {
        private readonly TypeReference genericEnumSerializerType;

        public CecilEnumSerializerFactory(TypeReference genericEnumSerializerType)
        {
            this.genericEnumSerializerType = genericEnumSerializerType;
        }

        public TypeReference GetSerializer(TypeReference objectType)
        {
            var resolvedObjectType = objectType.Resolve();
            if (resolvedObjectType != null && resolvedObjectType.IsEnum)
            {
                return genericEnumSerializerType.MakeGenericType(objectType);
            }

            return null;
        }
    }
}
