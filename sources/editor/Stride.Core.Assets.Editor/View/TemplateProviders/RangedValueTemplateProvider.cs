// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core.Assets.Editor.Quantum.NodePresenters.Keys;
using Stride.Core.Reflection;
using Stride.Core.Presentation.Quantum.View;
using Stride.Core.Presentation.Quantum.ViewModels;

namespace Stride.Core.Assets.Editor.View.TemplateProviders
{
    public class RangedValueTemplateProvider : NodeViewModelTemplateProvider
    {
        public override string Name => "RangedValueTemplateProvider";

        public override bool MatchNode(NodeViewModel node)
        {
            // We need at least a minimum and a maximum to display a slider, but we also rely on having explicit small and large steps to make sure that the
            // slider won't be between the whole integer range for instance.
            return node.Type.IsNumeric() && node.AssociatedData.ContainsKey(NumericData.Minimum) && node.AssociatedData.ContainsKey(NumericData.Maximum)
                   && node.AssociatedData.ContainsKey(NumericData.SmallStep) && node.AssociatedData.ContainsKey(NumericData.LargeStep);
        }
    }
}
