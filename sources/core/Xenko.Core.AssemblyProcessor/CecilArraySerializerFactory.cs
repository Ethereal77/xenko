// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Mono.Cecil;

namespace Xenko.Core.AssemblyProcessor
{
    /// <summary>
    /// Generates array serializer type from a given array type.
    /// </summary>
    public class CecilArraySerializerFactory : ICecilSerializerFactory
    {
        private readonly TypeReference genericArraySerializerType;

        public CecilArraySerializerFactory(TypeReference genericArraySerializerType)
        {
            this.genericArraySerializerType = genericArraySerializerType;
        }

        public TypeReference GetSerializer(TypeReference objectType)
        {
            if (objectType.IsArray)
            {
                return genericArraySerializerType.MakeGenericType(((ArrayType)objectType).ElementType);
            }

            return null;
        }
    }
}
