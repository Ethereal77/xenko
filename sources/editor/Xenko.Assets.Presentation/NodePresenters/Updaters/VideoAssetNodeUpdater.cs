// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Linq;

using Xenko.Core.Assets.Editor.Quantum.NodePresenters;
using Xenko.Core.Assets.Editor.Quantum.NodePresenters.Keys;
using Xenko.Assets.Media;

namespace Xenko.Assets.Presentation.NodePresenters.Updaters
{
    internal sealed class VideoAssetNodeUpdater : AssetNodePresenterUpdaterBase
    {
        private const string AbsoluteWidth = nameof(AbsoluteWidth);
        private const string AbsoluteHeight = nameof(AbsoluteHeight);

        protected override void UpdateNode(IAssetNodePresenter node)
        {
            var asset = node.Asset?.Asset as VideoAsset;
            if (asset != null && node.Name == nameof(VideoAsset.Width))
            {
                node.IsVisible = asset.IsSizeInPercentage;

                var absoluteWidth = node.Parent.Children.FirstOrDefault(x => x.Name == AbsoluteWidth)
                                    ?? node.Factory.CreateVirtualNodePresenter(node.Parent, AbsoluteWidth, typeof(int), node.Order,
                                        () => node.Value, node.UpdateValue, () => node.HasBase, () => node.IsInherited, () => node.IsOverridden);
                absoluteWidth.IsVisible = !asset.IsSizeInPercentage;
                absoluteWidth.AttachedProperties.Set(NumericData.MinimumKey, 0);
                absoluteWidth.AttachedProperties.Set(NumericData.MaximumKey, float.MaxValue);
                absoluteWidth.AttachedProperties.Set(NumericData.DecimalPlacesKey, 0);
            }
            if (asset != null && node.Name == nameof(VideoAsset.Height))
            {
                node.IsVisible = asset.IsSizeInPercentage;

                var absoluteHeight = node.Parent.Children.FirstOrDefault(x => x.Name == AbsoluteHeight)
                                  ?? node.Factory.CreateVirtualNodePresenter(node.Parent, AbsoluteHeight, typeof(int), node.Order,
                                  () => node.Value, node.UpdateValue, () => node.HasBase, () => node.IsInherited, () => node.IsOverridden);
                absoluteHeight.IsVisible = !asset.IsSizeInPercentage;
                absoluteHeight.AttachedProperties.Set(NumericData.MinimumKey, 0);
                absoluteHeight.AttachedProperties.Set(NumericData.MaximumKey, float.MaxValue);
                absoluteHeight.AttachedProperties.Set(NumericData.DecimalPlacesKey, 0);
            }
        }
        protected override void FinalizeTree(IAssetNodePresenter root)
        {
            var asset = root.Asset?.Asset as VideoAsset;
            if (asset != null)
            {
                var size = CategoryData.ComputeCategoryNodeName("Size");
                root[size][nameof(VideoAsset.Width)].AddDependency(root[size][nameof(VideoAsset.IsSizeInPercentage)], false);
                root[size][nameof(VideoAsset.Height)].AddDependency(root[size][nameof(VideoAsset.IsSizeInPercentage)], false);
            }
        }
    }
}
