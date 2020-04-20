// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core.Assets.Editor.ViewModel;
using Stride.Core.Annotations;
using Stride.Core.Quantum;
using Stride.Assets.Navigation;
using Stride.Navigation;

namespace Stride.Assets.Presentation.ViewModel
{
    [AssetViewModel(typeof(NavigationMeshAsset))]
    public class NavigationViewModel : AssetViewModel<NavigationMeshAsset>
    {
        public NavigationViewModel([NotNull] AssetViewModelConstructionParameters parameters) : base(parameters)
        {
        }
    }
}
