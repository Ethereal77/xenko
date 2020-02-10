// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.Generic;

using Xenko.Core.Assets.Editor.ViewModel;
using Xenko.Core.Annotations;
using Xenko.Core.Diagnostics;
using Xenko.Assets.Presentation.AssetEditors.UIEditor.Services;
using Xenko.Assets.Presentation.AssetEditors.UIPageEditor.ViewModels;
using Xenko.Assets.UI;
using Xenko.UI;

namespace Xenko.Assets.Presentation.AssetEditors.UIPageEditor.Services
{
    /// <summary>
    /// Game controller for the UI page editor.
    /// </summary>
    public sealed class UIPageEditorController : UIEditorController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UIEditorController"/> class.
        /// </summary>
        /// <param name="asset">The UI page associated with this instance.</param>
        /// <param name="editor">The editor associated with this instance.</param>
        public UIPageEditorController([NotNull] AssetViewModel asset, [NotNull] UIPageEditorViewModel editor)
            : base(asset, editor)
        {
        }

        /// <inheritdoc/>
        protected override bool ConstructRootElements(out ICollection<UIElement> rootElements, out UIAssetBase.UIDesign editorSettings)
        {
            var uiAsset = (UIAssetBase)Asset.Asset;
            if (uiAsset.Hierarchy.RootParts.Count > 1)
            {
                Editor.Logger.Error(UIPageRootViewModel.OneRootOnly);
                rootElements = null;
                editorSettings = null;
                return false;
            }

            return base.ConstructRootElements(out rootElements, out editorSettings);
        }
    }
}
