// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;

using Stride.Core;

namespace Stride.Graphics
{
    public class MutablePipelineState
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly Dictionary<PipelineStateDescriptionWithHash, PipelineState> cache;

        public PipelineStateDescription State;


        /// <summary>
        ///   Current compiled <see cref="PipelineState"/>.
        /// </summary>
        public PipelineState CurrentState;


        public MutablePipelineState(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;

            cache = graphicsDevice.GetOrCreateSharedData(typeof(MutablePipelineStateCache), device => new MutablePipelineStateCache()).Cache;

            State = new PipelineStateDescription();
            State.SetDefaults();
        }


        /// <summary>
        ///   Determines and updates the <see cref="CurrentState"/> from <see cref="State"/>.
        /// </summary>
        public void Update()
        {
            // Hash current state
            var hashedState = new PipelineStateDescriptionWithHash(State);

            // Find existing PipelineState object
            PipelineState pipelineState;

            // TODO: GRAPHICS REFACTOR We could avoid lock by adding them to a ThreadLocal (or RenderContext) and merge at end of frame
            lock (cache)
            {
                if (!cache.TryGetValue(hashedState, out pipelineState))
                {
                    // Otherwise, instantiate it
                    // First, make an copy
                    hashedState = new PipelineStateDescriptionWithHash(State.Clone());
                    cache.Add(hashedState, pipelineState = PipelineState.New(graphicsDevice, ref State));
                }
            }

            CurrentState = pipelineState;
        }

        private class MutablePipelineStateCache : IDisposable
        {
            public readonly Dictionary<PipelineStateDescriptionWithHash, PipelineState> Cache = new Dictionary<PipelineStateDescriptionWithHash, PipelineState>();

            public void Dispose()
            {
                foreach (var pipelineState in Cache)
                {
                    ((IReferencable)pipelineState.Value).Release();
                }

                Cache.Clear();
            }
        }
    }
}
