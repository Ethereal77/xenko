// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Core.Assets.Editor.ViewModel;
using Stride.Core;

namespace Stride.Core.Assets.Editor.Quantum.NodePresenters.Keys
{
    public static class OwnerAssetData
    {
        public const string OwnerAsset = nameof(OwnerAsset);
        public static readonly PropertyKey<AssetViewModel> Key = new PropertyKey<AssetViewModel>(OwnerAsset, typeof(OwnerAssetData));
    }
}
