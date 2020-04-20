// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace Stride.Core
{
    /// <summary>
    /// When specified on a property or field, a serializer won't be needed for this type (useful if serializer is dynamically or manually registered).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DataMemberCustomSerializerAttribute : Attribute
    {        
    }
}
