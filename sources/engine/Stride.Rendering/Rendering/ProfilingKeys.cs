// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core.Diagnostics;

namespace Stride.Rendering
{
    /// <summary>
    /// Various <see cref="ProfilingKey"/> used to measure performance across some part of the effect system.
    /// </summary>
    public class ProfilingKeys
    {
        public static readonly ProfilingKey Engine = new ProfilingKey("Engine");

        public static readonly ProfilingKey ModelRenderProcessor = new ProfilingKey(Engine, "ModelRenderer");

        public static readonly ProfilingKey PrepareMesh = new ProfilingKey(ModelRenderProcessor, "PrepareMesh");

        public static readonly ProfilingKey RenderMesh = new ProfilingKey(ModelRenderProcessor, "RenderMesh");

        public static readonly ProfilingKey AnimationProcessor = new ProfilingKey(Engine, "AnimationProcessor");
    }
}
