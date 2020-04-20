﻿// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2015 SharpYaml - Alexandre Mutel
// Copyright (c) 2008-2012 YamlDotNet - Antoine Aubry
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core.Yaml.Events;

namespace Stride.Core.Yaml
{
    /// <summary>
    /// Represents a YAML stream emitter.
    /// </summary>
    public interface IEmitter
    {
        /// <summary>
        /// Emits an event.
        /// </summary>
        void Emit(ParsingEvent @event);
    }
}