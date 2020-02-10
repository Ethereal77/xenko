// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

using Xenko.Core.Assets.Editor.ViewModel.CopyPasteProcessors;
using Xenko.Assets.Entities;

namespace Xenko.Assets.Presentation.ViewModel.CopyPasteProcessors
{
    internal class ScenePostPasteProcessor : AssetPostPasteProcessorBase<SceneAsset>
    {
        /// <inheritdoc />
        protected override void PostPasteDeserialization(SceneAsset asset)
        {
            // Clear all references (for now)
            asset.Parent = null;
            asset.ChildrenIds.Clear();
        }
    }
}
