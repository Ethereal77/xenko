// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.Generic;

using Stride.Core.Presentation.Quantum.ViewModels;

namespace Stride.Core.Assets.Editor.ViewModel
{
    public abstract class AddReferenceViewModel : IAddReferenceViewModel
    {
        protected NodeViewModel TargetNode;

        public void SetTargetNode(NodeViewModel node)
        {
            TargetNode = node;
        }

        public abstract bool CanAddChildren(IReadOnlyCollection<object> children, AddChildModifiers modifiers, out string message);

        public abstract void AddChildren(IReadOnlyCollection<object> children, AddChildModifiers modifiers);
    }
}
