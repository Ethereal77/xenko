// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Editor.EditorGame.Game;

namespace Stride.Assets.Presentation.AssetEditors.EntityHierarchyEditor.Services
{
    public interface IEditorGameRenderModeService : IEditorGameService
    {
        /// <summary>
        /// Gets or sets the material filter stream.
        /// </summary>
        /// <value>The material filter stream.</value>
        EditorRenderMode RenderMode { get; set; }
    }
}
