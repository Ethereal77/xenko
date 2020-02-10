// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;

using Xenko.Engine;

namespace Xenko.Assets.Presentation.AssetEditors.EntityHierarchyEditor.Game
{
    /// <summary>
    /// Arguments of the events fired by <see cref="EditorGameEntitySelectionService"/> when the selection changed.
    /// </summary>
    public sealed class EntitySelectionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySelectionEventArgs"/> class.
        /// </summary>
        /// <param name="oldSelection">The previously selected entities.</param>
        /// <param name="newSelection">The new selected entities.</param>
        public EntitySelectionEventArgs(IReadOnlyCollection<Entity> oldSelection, IReadOnlyCollection<Entity> newSelection)
        {
            if (oldSelection == null) throw new ArgumentNullException(nameof(oldSelection));
            if (newSelection == null) throw new ArgumentNullException(nameof(newSelection));
            OldSelection = oldSelection;
            NewSelection = newSelection;
        }

        /// <summary>
        /// Gets the previously selected entities.
        /// </summary>
        public IReadOnlyCollection<Entity> OldSelection { get; private set; }

        /// <summary>
        /// Gets the new selected entities.
        /// </summary>
        public IReadOnlyCollection<Entity> NewSelection { get; private set; }
    }
}
