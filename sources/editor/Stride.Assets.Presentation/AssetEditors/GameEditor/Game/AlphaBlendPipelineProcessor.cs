// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Graphics;
using Stride.Rendering;

namespace Stride.Assets.Presentation.AssetEditors.GameEditor.Game
{
    public class AlphaBlendPipelineProcessor : PipelineProcessor
    {
        public RenderStage RenderStage { get; set; }

        public override void Process(RenderNodeReference renderNodeReference, ref RenderNode renderNode, RenderObject renderObject, PipelineStateDescription pipelineState)
        {
            if (renderNode.RenderStage == RenderStage)
            {
                pipelineState.BlendState = BlendStates.AlphaBlend;
                pipelineState.DepthStencilState = DepthStencilStates.DepthRead;
            }
        }
    }
}
