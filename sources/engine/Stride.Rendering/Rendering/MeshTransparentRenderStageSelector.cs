// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.ComponentModel;

using Stride.Engine;

namespace Stride.Rendering
{
    public class MeshTransparentRenderStageSelector : TransparentRenderStageSelector
    {
        public override void Process(RenderObject renderObject)
        {
            if (((RenderGroupMask)(1U << (int)renderObject.RenderGroup) & RenderGroup) != 0)
            {
                var renderMesh = (RenderMesh)renderObject;

                var renderStage = renderMesh.MaterialPass.HasTransparency ? TransparentRenderStage : OpaqueRenderStage;
                if (renderStage != null)
                    renderObject.ActiveRenderStages[renderStage.Index] = new ActiveRenderStage(EffectName);
            }
        }
    }
}
