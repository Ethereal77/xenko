// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core.Presentation.Quantum.Presenters;

namespace Xenko.Core.Assets.Editor.Quantum.NodePresenters.Commands
{
    public class ResetOverrideCommand : SyncNodePresenterCommandBase
    {
        /// <summary>
        /// The name of this command.
        /// </summary>
        public const string CommandName = "ResetOverride";

        /// <inheritdoc/>
        public override string Name => CommandName;

        /// <inheritdoc/>
        public override bool CanAttach(INodePresenter nodePresenter)
        {
            return nodePresenter is IAssetNodePresenter;
        }

        /// <inheritdoc/>
        protected override void ExecuteSync(INodePresenter nodePresenter, object parameter, object preExecuteResult)
        {
            var assetNodePresenter = (IAssetNodePresenter)nodePresenter;
            assetNodePresenter.ResetOverride();
        }
    }
}
