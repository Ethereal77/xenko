// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core.Presentation.Quantum.View;
using Xenko.Core.Presentation.Quantum.ViewModels;
using Xenko.Assets.Models;

namespace Xenko.Assets.Presentation.TemplateProviders
{
    public class AnimationFrameTemplateProvider : NodeViewModelTemplateProvider
    {
        public override string Name { get { return "AnimationFrameTemplateProvider"; } }

        public override bool MatchNode(NodeViewModel node)
        {
            return (node.Name.Equals(nameof(AnimationAssetDuration.StartAnimationTime)) || node.Name.Equals(nameof(AnimationAssetDuration.EndAnimationTime)));
        }
    }
}
