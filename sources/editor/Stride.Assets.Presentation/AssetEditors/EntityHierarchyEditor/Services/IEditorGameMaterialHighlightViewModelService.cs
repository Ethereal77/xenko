// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

using Stride.Assets.Presentation.AssetEditors.EntityHierarchyEditor.ViewModels;
using Stride.Assets.Presentation.AssetEditors.GameEditor.Services;
using Stride.Editor.EditorGame.ViewModels;

namespace Stride.Assets.Presentation.AssetEditors.EntityHierarchyEditor.Services
{
    public interface IEditorGameMaterialHighlightViewModelService : IEditorGameViewModelService
    {
        bool IsActive { get; set; }

        Tuple<Guid, int> GetTargetMeshIndex(EntityViewModel entity);
    }
}
