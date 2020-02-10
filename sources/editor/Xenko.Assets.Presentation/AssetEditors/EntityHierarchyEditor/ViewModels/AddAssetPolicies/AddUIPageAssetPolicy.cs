// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core.Assets.Editor.Services;
using Xenko.Core.Annotations;
using Xenko.Assets.UI;
using Xenko.Assets.Presentation.ViewModel;
using Xenko.Engine;

namespace Xenko.Assets.Presentation.AssetEditors.EntityHierarchyEditor.ViewModels
{
    internal class AddUIPageAssetPolicy : CreateComponentPolicyBase<UIPageAsset, UIPageViewModel>
    {
        /// <inheritdoc />
        [NotNull]
        protected override EntityComponent CreateComponentFromAsset(EntityHierarchyItemViewModel parent, UIPageViewModel asset)
        {
            return new UIComponent
            {
                Page = ContentReferenceHelper.CreateReference<UIPage>(asset)
            };
        }
    }
}
