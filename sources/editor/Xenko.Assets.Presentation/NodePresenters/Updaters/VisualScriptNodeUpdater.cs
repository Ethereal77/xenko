// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Linq;

using Xenko.Core.Assets.Editor.Quantum.NodePresenters;
using Xenko.Core.Assets.Editor.Quantum.NodePresenters.Keys;
using Xenko.Core.Presentation.Quantum.Presenters;
using Xenko.Assets.Presentation.AssetEditors.VisualScriptEditor;
using Xenko.Assets.Presentation.NodePresenters.Keys;
using Xenko.Assets.Presentation.ViewModel;
using Xenko.Assets.Scripts;

namespace Xenko.Assets.Presentation.NodePresenters.Updaters
{
    internal sealed class VisualScriptNodeUpdater : AssetNodePresenterUpdaterBase
    {
        public const string OwnerAsset = nameof(OwnerAsset);

        protected override void UpdateNode(IAssetNodePresenter node)
        {
            var provider = node.PropertyProvider as VisualScriptBlockViewModel;
            if (provider == null)
                return;

            var isBlock = typeof(Block).IsAssignableFrom(node.Type);
            if (isBlock)
            {
                node.AttachedProperties.Add(VisualScriptData.OwnerBlockKey, provider);
            }

            var memberNode = node as MemberNodePresenter;
            var isVariableReference = node.Type == typeof(string) && memberNode != null && memberNode.MemberAttributes.OfType<ScriptVariableReferenceAttribute>().Any();
            if (isVariableReference)
            {
                node.AttachedProperties.Add(ReferenceData.Key, new SymbolReferenceViewModel());
            }
        }
    }
}
