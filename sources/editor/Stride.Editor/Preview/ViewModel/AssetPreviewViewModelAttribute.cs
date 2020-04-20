// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

using Stride.Core.Annotations;

namespace Stride.Editor.Preview.ViewModel
{
    [AttributeUsage(AttributeTargets.Class)]
    [BaseTypeRequired(typeof(IAssetPreviewViewModel))]
    public sealed class AssetPreviewViewModelAttribute : Attribute
    {
        public AssetPreviewViewModelAttribute(Type assetPreviewType)
        {
            if (assetPreviewType == null) throw new ArgumentNullException(nameof(assetPreviewType));

            AssetPreviewType = assetPreviewType;
        }

        public Type AssetPreviewType { get; set; }
    }
}
