// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core.Assets.Editor.ViewModel;

namespace Stride.Editor.Preview.ViewModel
{
    /// <summary>
    /// An interface that represents a view model that can be attached to an <see cref="IAssetPreview"/>.
    /// </summary>
    /// <remarks>Implementation should provide at least one constructor with a <see cref="SessionViewModel"/> as first argument.</remarks>
    public interface IAssetPreviewViewModel
    {
        SessionViewModel Session { get; }

        /// <summary>
        /// Attaches the given preview to this view model.
        /// </summary>
        /// <param name="preview">The preview to attach.</param>
        void AttachPreview(IAssetPreview preview);
    }
}
