﻿// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2015 SharpYaml - Alexandre Mutel
// Copyright (c) 2008-2012 YamlDotNet - Antoine Aubry
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace Stride.Core.Yaml.Serialization
{
    /// <summary>
    /// Creates instances of types.
    /// </summary>
    /// <remarks>
    /// This interface allows to provide a custom logic for creating instances during deserialization.
    /// </remarks>
    public interface IObjectFactory
    {
        /// <summary>
        /// Creates an instance of the specified type. Returns null if instance cannot be created.
        /// </summary>
        object Create(Type type);
    }
}