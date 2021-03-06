// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

using Stride.Core;
using Stride.Core.Annotations;
using Stride.Core.Extensions;
using Stride.Core.Mathematics;
using Stride.Core.BuildEngine;
using Stride.Input;
using Stride.Rendering;
using Stride.Rendering.Sprites;
using Stride.Rendering.Compositing;
using Stride.Engine;
using Stride.Assets.Presentation.AssetEditors.SceneEditor.ViewModels;
using Stride.Assets.Presentation.AssetEditors.EntityHierarchyEditor.ViewModels;
using Stride.Assets.Presentation.AssetEditors.Gizmos;
using Stride.Assets.Presentation.AssetEditors.GameEditor.Services;
using Stride.Assets.Presentation.AssetEditors.GameEditor.Game;
using Stride.Assets.Presentation.SceneEditor;
using Stride.Editor.EditorGame.Game;

namespace Stride.Assets.Presentation.AssetEditors.EntityHierarchyEditor.Game
{
    /// <summary>
    ///   Represents a service that manages the selection in the Entity Hierarchy Editor, providing methods to modify
    ///   the selection from the game thread and handle changes in the selection that occurs in the view model.
    /// </summary>
    public class EditorGameEntitySelectionService : EditorGameMouseServiceBase, IEditorGameEntitySelectionService, IEditorGameSelectionViewModelService
    {
        private Vector2 mouseMoveAccumulator;
        private PickingSceneRenderer entityPicker;
        private EntityHierarchyEditorGame game;

        /// <summary>
        ///   Initializes a new instance of the <see cref="EditorGameEntitySelectionService"/> class.
        /// </summary>
        /// <param name="editor">The <see cref="EntityHierarchyEditorViewModel"/> related to the current instance of the scene editor.</param>
        public EditorGameEntitySelectionService([NotNull] EntityHierarchyEditorViewModel editor)
        {
            if (editor is null)
                throw new ArgumentNullException(nameof(editor));

            Editor = editor;
        }

        /// <inheritdoc/>
        public override bool IsControllingMouse { get; protected set; }

        /// <summary>
        ///   Gets the number of currently selected entities.
        /// </summary>
        public int SelectedIdCount => SelectedIds.Count;

        /// <summary>
        ///   Gets the number of currently selected root entities.
        /// </summary>
        /// <remarks>
        ///   An entity is selected as a root if it is currently selected and none of its parent is currently selected.
        /// </remarks>
        public int SelectedRootIdCount => SelectedRootIds.Count;

        /// <summary>
        ///   Raised in the scene game thread after the selection has been updated.
        /// </summary>
        public event EventHandler<EntitySelectionEventArgs> SelectionUpdated;

        /// <inheritdoc/>
        public override IEnumerable<Type> Dependencies { get { yield return typeof(IEditorGameComponentGizmoService); } }

        /// <summary>
        ///   Gets the scene editor this service is operating on.
        /// </summary>
        protected EntityHierarchyEditorViewModel Editor { get; }

        /// <summary>
        ///   Gets a lock object that should be taken when accessing <see cref="SelectedIds"/> and <see cref="SelectedRootIds"/>.
        /// </summary>
        protected object LockObject { get; } = new object();

        protected ISet<AbsoluteId> SelectableIds { get; } = new HashSet<AbsoluteId>();

        /// <summary>
        ///   Gets the set of <see cref="AbsoluteId"/> corresponding to the currently selected entities.
        /// </summary>
        protected ISet<AbsoluteId> SelectedIds { get; } = new HashSet<AbsoluteId>();

        /// <summary>
        ///   Gets the set of <see cref="AbsoluteId"/> corresponding to the currently selected root entities.
        /// </summary>
        /// <remarks>
        ///   An entity is selected as a root if it is currently selected and none of its parent is currently selected.
        /// </remarks>
        protected ISet<AbsoluteId> SelectedRootIds { get; } = new HashSet<AbsoluteId>();

        /// <inheritdoc/>
        bool IEditorGameSelectionViewModelService.DisplaySelectionMask { get; set; }

        /// <inheritdoc/>
        bool IEditorGameEntitySelectionService.DisplaySelectionMask => ((IEditorGameSelectionViewModelService)this).DisplaySelectionMask;

        private IEditorGameComponentGizmoService Gizmos => Services.Get<IEditorGameComponentGizmoService>();

        /// <inheritdoc />
        public override ValueTask DisposeAsync()
        {
            EnsureNotDestroyed(nameof(EditorGameEntitySelectionService));

            Editor.SelectedContent.CollectionChanged -= SelectedContentChanged;
            SelectionUpdated -= OnSelectionUpdated;

            return base.DisposeAsync();
        }

        /// <inheritdoc/>
        public override void RegisterScene(Scene scene)
        {
            base.RegisterScene(scene);

            entityPicker.CacheScene(scene, isRecursive: true);

            game.SceneSystem.SceneInstance.EntityAdded += (_, entity) => entityPicker.CacheEntity(entity, isRecursive: false);
            game.SceneSystem.SceneInstance.EntityRemoved += (_, entity) => entityPicker.UncacheEntity(entity, isRecursive: false);
            game.SceneSystem.SceneInstance.ComponentChanged += (_, e) =>
            {
                if (e.PreviousComponent is not null)
                    entityPicker.UncacheEntityComponent(e.PreviousComponent);
                if (e.NewComponent is not null)
                    entityPicker.CacheEntityComponent(e.NewComponent);
            };

            SelectionUpdated += OnSelectionUpdated;
        }

        /// <summary>
        ///   Gets a copy of the set of <see cref="AbsoluteId"/> corresponding to the currently selected entities.
        /// </summary>
        [NotNull]
        public IReadOnlyCollection<AbsoluteId> GetSelectedIds()
        {
            lock (LockObject)
            {
                return SelectedIds.ToList();
            }
        }

        /// <summary>
        ///   Gets a copy of the set of <see cref="AbsoluteId"/> corresponding to the currently selected root entities.
        /// </summary>
        /// <remarks>
        ///   An entity is selected as a root if it is currently selected and none of its parent is currently selected.
        /// </remarks>
        [NotNull]
        public IReadOnlyCollection<AbsoluteId> GetSelectedRootIds()
        {
            lock (LockObject)
            {
                return SelectedRootIds.ToList();
            }
        }

        /// <summary>
        ///   Clears the selection.
        /// </summary>
        private void Clear()
        {
            if (SelectedIds.Count == 0)
                return;

            IsControllingMouse = true;
            Editor.Dispatcher.InvokeAsync(() =>
            {
                Editor.ClearSelection();
                Editor.Controller.InvokeAsync(() => IsControllingMouse = false);
            });
        }

        /// <summary>
        ///   Selects an entity.
        /// </summary>
        /// <param name="entity">The entity to select.</param>
        private void Set([NotNull] Entity entity)
        {
            var entityId = Editor.Controller.GetAbsoluteId(entity);

            if (!SelectableIds.Contains(entityId) || SelectedIds.Count == 1 && SelectedIds.Contains(entityId))
                return;

            IsControllingMouse = true;

            Editor.Dispatcher.InvokeAsync(() =>
            {
                var viewModel = (EntityHierarchyElementViewModel) Editor.FindPartViewModel(entityId);

                Editor.ClearSelection();

                if (viewModel is not null)
                    Editor.SelectedContent.Add(viewModel);

                Editor.Controller.InvokeAsync(() => IsControllingMouse = false);
            });
        }

        /// <summary>
        ///   Adds an entity to the selection.
        /// </summary>
        /// <param name="entity">The entity to add to the selection.</param>
        private void Add([NotNull] Entity entity)
        {
            var entityId = Editor.Controller.GetAbsoluteId(entity);

            if (!SelectableIds.Contains(entityId) || SelectedIds.Contains(entityId))
                return;

            IsControllingMouse = true;

            Editor.Dispatcher.InvokeAsync(() =>
            {
                var viewModel = (EntityHierarchyElementViewModel) Editor.FindPartViewModel(entityId);
                if (viewModel?.IsSelectable == true)
                    Editor.SelectedContent.Add(viewModel);

                Editor.Controller.InvokeAsync(() => IsControllingMouse = false);
            });
        }

        /// <summary>
        ///   Removes an entity from the selection.
        /// </summary>
        /// <param name="entity">The entity to remove from the selection.</param>
        private void Remove([NotNull] Entity entity)
        {
            var entityId = Editor.Controller.GetAbsoluteId(entity);

            if (!SelectedIds.Contains(entityId))
                return;

            IsControllingMouse = true;

            Editor.Dispatcher.InvokeAsync(() =>
            {
                var viewModel = (EntityHierarchyElementViewModel) Editor.FindPartViewModel(entityId);
                if (viewModel is not null)
                    Editor.SelectedContent.Remove(viewModel);

                Editor.Controller.InvokeAsync(() => IsControllingMouse = false);
            });
        }

        /// <summary>
        ///   Indicates whether an entity is currently selected.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <returns>
        ///   <c>true</c> if the entity is currently selected, <c>false</c> otherwise.
        /// </returns>
        private bool Contains([NotNull] Entity entity)
        {
            var entityId = Editor.Controller.GetAbsoluteId(entity);

            return SelectedIds.Contains(entityId);
        }

        /// <summary>
        ///   Tries to pick the <see cref="Entity"/> under the mouse cursor.
        /// </summary>
        /// <returns>An <see cref="EntityPickingResult"/> structure containing information of the picked <see cref="Entity"/>, if any.</returns>
        public EntityPickingResult Pick() => entityPicker?.Pick() ?? default;

        /// <inheritdoc/>
        public async Task<bool> DuplicateSelection()
        {
            var duplicatedIds = new HashSet<AbsoluteId>();
            var selectedEntitiesId = new HashSet<AbsoluteId>();

            var tcs = new TaskCompletionSource<bool>();

            void onSelectionUpdatedCallback(object _, EntitySelectionEventArgs e)
            {
                // Ignore first call (clear list)
                selectedEntitiesId.Clear();
                selectedEntitiesId.AddRange(e.NewSelection.Select(Editor.Controller.GetAbsoluteId));

                if (selectedEntitiesId.SetEquals(duplicatedIds))
                {
                    tcs.TrySetResult(selectedEntitiesId.SetEquals(duplicatedIds));
                }
            }

            SelectionUpdated += onSelectionUpdatedCallback;

            var result = await Editor.Dispatcher.InvokeAsync(() => Editor.DuplicateSelectedEntities()?.Select(x => x.Id));
            if (result is null)
            {
                SelectionUpdated -= onSelectionUpdatedCallback;
                return false;
            }
            duplicatedIds.AddRange(result);

            await tcs.Task;
            SelectionUpdated -= onSelectionUpdatedCallback;
            return true;
        }

        //
        // Adds an element (Entity) to the current selection.
        //
        private void AddToSelection([NotNull] EntityHierarchyElementViewModel element)
        {
            lock (LockObject)
            {
                if (!element.IsSelectable)
                    return;

                // Add the Entity id to the selected ids
                SelectedIds.Add(element.Id);

                // Check if one of its parents is in the selection
                var parent = element.TransformParent;
                while (parent is not null)
                {
                    if (SelectedIds.Contains(parent.Id))
                        break;

                    parent = parent.TransformParent;
                }

                // If so, the SelectedRootIds collection does not need to be updated.
                if (parent is not null)
                    return;

                // Otherwise, it's a new root entity in the selection.
                SelectedRootIds.Add(element.Id);

                // Remove its children that were previously root entities in the selection.
                foreach (var child in element.TransformChildren.SelectDeep(x => x.TransformChildren))
                {
                    SelectedRootIds.Remove(child.Id);
                }
            }
        }

        //
        // Removes an element (Entity) from the current selection.
        //
        private void RemoveFromSelection([NotNull] EntityHierarchyElementViewModel element)
        {
            lock (LockObject)
            {
                SelectedIds.Remove(element.Id);

                // Remove the root entity from the selected root entities
                if (SelectedRootIds.Remove(element.Id) && element.IsLoaded)
                {
                    // Ensure all children that are selected are properly added to the selected root collection
                    foreach (var child in element.TransformChildren.SelectDeep(x => x.TransformChildren).Where(x => SelectedIds.Contains(x.Id)))
                    {
                        // Check if one of its parents is in the selection
                        var parent = child.TransformParent;
                        while (parent != element && parent is not null)
                        {
                            if (SelectedIds.Contains(parent.Id))
                                break;

                            parent = parent.TransformParent;
                        }

                        // If so, the SelectedRootIds collection does not need to be updated.
                        if (parent != element)
                            return;

                        // Otherwise, it's a new root entity in the selection.
                        SelectedRootIds.Add(child.Id);
                    }
                }
            }
        }

        //
        // Called when the contents of the selection have changed.
        //
        private void SelectedContentChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!Editor.SceneInitialized)
                return;

            lock (LockObject)
            {
                // Retrieve old selection to pass it to the event
                var oldSelectionIds = GetSelectedRootIds();

                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (EntityHierarchyItemViewModel newItem in e.NewItems)
                        {
                            if (newItem is SceneRootViewModel root)
                                AddToSelection(root);
                            else
                                foreach (var entity in newItem.InnerSubEntities)
                                    AddToSelection(entity);
                        }
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        foreach (EntityHierarchyItemViewModel oldItem in e.OldItems)
                        {
                            if (oldItem is SceneRootViewModel root)
                                RemoveFromSelection(root);
                            else
                                foreach (var entity in oldItem.InnerSubEntities)
                                    RemoveFromSelection(entity);
                        }
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        SelectedIds.Clear();
                        SelectedRootIds.Clear();
                        break;

                    case NotifyCollectionChangedAction.Replace:
                    case NotifyCollectionChangedAction.Move:
                        throw new NotSupportedException("This operation is not supported.");

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                RaiseSelectionUpdated(oldSelectionIds);
            }
        }

        //
        // Raises the SelectionUpdated event with the changes in the selection.
        //
        private void RaiseSelectionUpdated(IReadOnlyCollection<AbsoluteId> oldSelectionIds)
        {
            var newSelectionIds = GetSelectedRootIds();

            Editor.Controller.InvokeAsync(() =>
            {
                var oldSelection = oldSelectionIds.Select(x => Editor.Controller.FindGameSidePart(x)).Cast<Entity>().NotNull().ToList();
                var newSelection = newSelectionIds.Select(x => Editor.Controller.FindGameSidePart(x)).Cast<Entity>().NotNull().ToList();

                SelectionUpdated?.Invoke(this, new EntitySelectionEventArgs(oldSelection, newSelection));
            });
        }

        //
        // Checks the changes in the selected elements and updates the gizmos.
        //
        private void OnSelectionUpdated(object sender, [NotNull] EntitySelectionEventArgs e)
        {
            var previousSelection = new HashSet<Entity>(e.OldSelection);
            foreach (var childEntity in e.OldSelection.SelectDeep(x => x.Transform.Children.Select(y => y.Entity)))
            {
                previousSelection.Add(childEntity);
            }

            var newSelection = new HashSet<Entity>(e.NewSelection);
            foreach (var childEntity in e.NewSelection.SelectDeep(x => x.Transform.Children.Select(y => y.Entity)))
            {
                newSelection.Add(childEntity);
            }

            previousSelection.ExceptWith(newSelection);

            // Update the selection on the gizmo entities
            foreach (var previousEntity in previousSelection)
            {
                UpdateGizmoEntitiesSelection(previousEntity, isSelected: false);
            }

            foreach (var newEntity in newSelection)
            {
                UpdateGizmoEntitiesSelection(newEntity, isSelected: true);
            }
        }

        //
        // Updates the gizmo selection state of an Entity.
        //
        private void UpdateGizmoEntitiesSelection([NotNull] Entity entity, bool isSelected)
        {
            Gizmos.UpdateGizmoEntitiesSelection(entity, isSelected);

            foreach (var child in entity.Transform.Children.SelectDeep(x => x.Children).Select(x => x.Entity).NotNull())
            {
                Gizmos.UpdateGizmoEntitiesSelection(child, isSelected);
            }
        }

        //
        // Main loop of this service.
        //
        private async Task Execute()
        {
            MicrothreadLocalDatabases.MountCommonDatabase();

            while (game.IsRunning)
            {
                await game.Script.NextFrame();

                if (IsActive)
                {
                    // TODO: Code largely duplicated in EditorGameMaterialHighlightService. Factorize!

                    var screenSize = new Vector2(game.GraphicsDevice.Presenter.BackBuffer.Width,
                                                 game.GraphicsDevice.Presenter.BackBuffer.Height);

                    if (game.Input.IsMouseButtonPressed(MouseButton.Left))
                    {
                        mouseMoveAccumulator = Vector2.Zero;
                    }
                    mouseMoveAccumulator += new Vector2(Math.Abs(game.Input.MouseDelta.X * screenSize.X),
                                                        Math.Abs(game.Input.MouseDelta.Y * screenSize.Y));

                    if (IsMouseAvailable && game.Input.IsMouseButtonReleased(MouseButton.Left) && !game.Input.IsMouseButtonDown(MouseButton.Right))
                    {
                        if (mouseMoveAccumulator.Length() >= TransformationGizmo.TransformationStartPixelThreshold)
                            continue;

                        var addToSelection = game.Input.IsKeyDown(Keys.LeftCtrl) || game.Input.IsKeyDown(Keys.RightCtrl);

                        var entityUnderMouse = Gizmos.GetContentEntityUnderMouse();

                        if (entityUnderMouse is null)
                        {
                            var entityPicked = Pick();
                            entityUnderMouse = entityPicked.Entity;

                            // Check for instancing
                            var instancingComponent = entityUnderMouse?.Get<InstancingComponent>();
                            if (instancingComponent is not null && instancingComponent.Enabled && instancingComponent.Type is InstancingEntityTransform instancing)
                            {
                                entityUnderMouse = instancing.GetInstanceAt(entityPicked.InstanceId)?.Entity ?? entityUnderMouse;
                            }
                        }

                        // Ctrl + click on an empty area: Do nothing
                        if (entityUnderMouse is null && addToSelection)
                            continue;

                        // Click on an empty area: Clear selection
                        if (entityUnderMouse is null)
                            Clear();

                        // Click on an entity: Select this entity
                        else if (!addToSelection)
                            Set(entityUnderMouse);
                        // Ctrl + click on an already selected entity: Unselect this entity
                        else if (Contains(entityUnderMouse))
                            Remove(entityUnderMouse);
                        // Ctrl + click on an entity: Add this entity to the selection
                        else
                            Add(entityUnderMouse);
                    }
                }
            }
        }

        /// <inheritdoc/>
        protected override Task<bool> Initialize(EditorServiceGame editorGame)
        {
            game = (EntityHierarchyEditorGame) editorGame;

            Editor.SelectedContent.CollectionChanged += SelectedContentChanged;
            game.Script.AddTask(Execute);
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public override void UpdateGraphicsCompositor(EditorServiceGame game)
        {
            base.UpdateGraphicsCompositor(game);

            var pickingRenderStage = new RenderStage("Picking", "Picking");
            game.SceneSystem.GraphicsCompositor.RenderStages.Add(pickingRenderStage);

            pickingRenderStage.Filter = new PickingFilter(this);

            // Meshes
            var meshRenderFeature = game.SceneSystem.GraphicsCompositor.RenderFeatures.OfType<MeshRenderFeature>().FirstOrDefault();
            // TODO: Complain (log) if there is no MeshRenderFeature
            if (meshRenderFeature is not null)
            {
                meshRenderFeature.RenderFeatures.Add(new PickingRenderFeature());
                meshRenderFeature.RenderStageSelectors.Add(new SimpleGroupToRenderStageSelector
                {
                    EffectName = EditorGraphicsCompositorHelper.EditorForwardShadingEffect + ".Picking",
                    RenderStage = pickingRenderStage,
                    RenderGroup = RenderGroupMask.All
                });
            }

            // Sprites
            var spriteRenderFeature = game.SceneSystem.GraphicsCompositor.RenderFeatures.OfType<SpriteRenderFeature>().FirstOrDefault();
            // TODO: Complain (log) if there is no SpriteRenderFeature
            if (spriteRenderFeature is not null)
            {
                spriteRenderFeature.RenderStageSelectors.Add(new SimpleGroupToRenderStageSelector
                {
                    EffectName = "Test",
                    RenderStage = pickingRenderStage,
                    RenderGroup = RenderGroupMask.All
                });
            }

            // TODO: SpriteStudio (not here but as a plugin)

            var editorCompositor = (EditorTopLevelCompositor)game.SceneSystem.GraphicsCompositor.Game;
            editorCompositor.PostGizmoCompositors.Add(entityPicker = new PickingSceneRenderer { PickingRenderStage = pickingRenderStage });

            var contentScene = ((EntityHierarchyEditorGame)game).ContentScene;
            if (contentScene is not null)
                entityPicker.CacheScene(contentScene, true);
        }

        /// <inheritdoc/>
        void IEditorGameSelectionViewModelService.AddSelectable(AbsoluteId id)
        {
            Editor.Controller.EnsureAssetAccess();
            Editor.Controller.InvokeAsync(() => SelectableIds.Add(id));

            // Add to game selection, in case it is already selected
            var element = (EntityHierarchyElementViewModel) Editor.FindPartViewModel(id);
            if (element is null)
                return;

            if (Editor.SelectedContent.Contains(element))
            {
                // Retrieve old selection to pass it to the event
                var oldSelectionIds = GetSelectedRootIds();
                AddToSelection(element);
                RaiseSelectionUpdated(oldSelectionIds);
            }
        }

        /// <inheritdoc/>
        void IEditorGameSelectionViewModelService.RemoveSelectable(AbsoluteId id)
        {
            Editor.Controller.EnsureAssetAccess();
            Editor.Controller.InvokeAsync(() => SelectableIds.Remove(id));

            // Remove from game selection, in case it was selected
            var element = (EntityHierarchyElementViewModel) Editor.FindPartViewModel(id);
            if (element is null)
                return;

            // Retrieve old selection to pass it to the event
            var oldSelectionIds = GetSelectedRootIds();
            RemoveFromSelection(element);
            RaiseSelectionUpdated(oldSelectionIds);
        }

        private class PickingFilter : RenderStageFilter
        {
            private readonly EditorGameEntitySelectionService service;

            public PickingFilter(EditorGameEntitySelectionService service)
            {
                this.service = service;
            }

            public override bool IsVisible(RenderObject renderObject, RenderView renderView, RenderViewStage renderViewStage)
            {
                var entity = (renderObject.Source as EntityComponent)?.Entity;
                if (entity is not null)
                {
                    var entityId = service.Editor.Controller.GetAbsoluteId(entity);
                    return service.SelectableIds.Contains(entityId);
                }

                return false;
            }
        }
    }
}
