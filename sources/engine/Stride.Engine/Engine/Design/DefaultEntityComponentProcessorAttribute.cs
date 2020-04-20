// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

using Stride.Core.Annotations;

namespace Stride.Engine.Design
{
    /// <summary>
    /// An attribute used to associate a default <see cref="EntityProcessor"/> to an entity component.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DefaultEntityComponentProcessorAttribute : DynamicTypeAttributeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultEntityComponentProcessorAttribute"/> class.
        /// </summary>
        /// <param name="type">The type must derived from <see cref="EntityProcessor"/>.</param>
        public DefaultEntityComponentProcessorAttribute(Type type) : base(type)
        {
        }

        public ExecutionMode ExecutionMode { get; set; } = ExecutionMode.All;
    }
} 
