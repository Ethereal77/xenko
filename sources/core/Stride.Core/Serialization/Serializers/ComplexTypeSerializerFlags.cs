// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace Stride.Core.Serialization.Serializers
{
    [Flags]
    public enum ComplexTypeSerializerFlags
    {
        SerializePublicFields = 1,
        SerializeNonPublicFields = 2,
        SerializeFields = SerializePublicFields | SerializeNonPublicFields,
        SerializePublicProperties = 4,
    }
}
