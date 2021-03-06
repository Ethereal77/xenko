// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Threading.Tasks;

using Stride.Assets.Presentation.AssetEditors.EntityHierarchyEditor.Game;
using Stride.Assets.Presentation.AssetEditors.Gizmos;
using Stride.Editor.EditorGame.Game;

namespace Stride.Assets.Presentation.AssetEditors.GameEditor.Game
{
    public class EditorGameSpaceMarkerService : EditorGameServiceBase
    {
        private EntityHierarchyEditorGame game;
        private SpaceMarker spaceMarker;

        protected override Task<bool> Initialize(EditorServiceGame editorGame)
        {
            game = (EntityHierarchyEditorGame)editorGame;
            spaceMarker = new SpaceMarker(game);
            spaceMarker.Initialize(game.Services, game.EditorScene);
            game.Script.AddTask(Update);
            return Task.FromResult(true);
        }

        private async Task Update()
        {
            // update all gizmo of the scene.
            while (!IsDisposed)
            {
                spaceMarker.Update();
                await game.Script.NextFrame();
            }
        }
    }
}
