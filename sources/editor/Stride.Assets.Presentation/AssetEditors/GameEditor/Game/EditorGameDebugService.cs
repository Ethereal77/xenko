// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Threading.Tasks;

using Stride.Core.Serialization.Contents;
using Stride.Assets.Presentation.AssetEditors.GameEditor.Services;
using Stride.Assets.Presentation.SceneEditor;
using Stride.Editor.EditorGame.Game;

namespace Stride.Assets.Presentation.AssetEditors.GameEditor.Game
{
    /// <summary>
    /// A class that provides access to debug information of an editor game.
    /// </summary>
    public class EditorGameDebugService : EditorGameServiceBase, IEditorGameDebugViewModelService
    {
        private Engine.Game game;

        /// <summary>
        /// Gets the stats of the scene editor asset manager.
        /// </summary>
        public ContentManagerStats ContentManagerStats => game.Content.GetStats();

        /// <inheritdoc/>
        protected override Task<bool> Initialize(EditorServiceGame editorGame)
        {
            game = editorGame;
            return Task.FromResult(true);
        }
    }
}
