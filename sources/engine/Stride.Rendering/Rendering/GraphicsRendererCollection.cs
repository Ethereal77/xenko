// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core.Serialization;
using Stride.Core.Serialization.Serializers;

namespace Stride.Rendering
{
    /// <summary>
    /// A collection of <see cref="IGraphicsRenderer"/> that is itself a <see cref="IGraphicsRenderer"/> handling automatically
    /// <see cref="IGraphicsRenderer.Initialize"/> and <see cref="IGraphicsRenderer.Unload"/>.
    /// </summary>
    /// <typeparam name="T">Type of the <see cref="IGraphicsRenderer"/></typeparam>.
    [DataSerializer(typeof(ListAllSerializer<,>), Mode = DataSerializerGenericMode.TypeAndGenericArguments)]
    public abstract class GraphicsRendererCollection<T> : GraphicsRendererCollectionBase<T> where T : class, IGraphicsRenderer
    {
        protected override void DrawRenderer(RenderDrawContext context, T renderer)
        {
            renderer.Draw(context);
        }
    }
}
