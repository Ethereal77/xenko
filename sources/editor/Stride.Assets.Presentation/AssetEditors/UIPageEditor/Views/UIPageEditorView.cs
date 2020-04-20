// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core.Assets.Editor.ViewModel;
using Stride.Assets.Presentation.AssetEditors.UIEditor.ViewModels;
using Stride.Assets.Presentation.AssetEditors.UIEditor.Views;
using Stride.Assets.Presentation.AssetEditors.UIPageEditor.ViewModels;
using Stride.Assets.Presentation.ViewModel;

namespace Stride.Assets.Presentation.AssetEditors.UIPageEditor.Views
{
    public class UIPageEditorView : UIEditorView
    {
        protected override UIEditorBaseViewModel CreateEditorViewModel(AssetViewModel asset)
        {
            return UIPageEditorViewModel.Create((UIPageViewModel)asset);
        }
    }
}
