// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

namespace Stride.Core.Reflection
{
    /// <summary>
    /// Interface of a factory that can create instances of a type.
    /// </summary>
    public interface IObjectFactory
    {
        /// <summary>
        /// Creates a new instance of a type.
        /// </summary>
        /// <param name="type">The type of the instance to create.</param>
        /// <returns>A new default instance of a type.</returns>
        object New(Type type);
    }
}
