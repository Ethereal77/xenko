// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core.Annotations;
using Stride.Core.Reflection;
using Stride.Core.Quantum;
using Stride.Core.Quantum.References;
using Stride.Core.Assets.Quantum.Internal;

namespace Stride.Core.Assets.Quantum
{
    /// <summary>
    ///   Represents a type of <see cref="GraphNodeLinker"/> that can link nodes of an asset to the corresponding nodes in their base.
    /// </summary>
    /// <remarks>
    ///   This class will invoke <see cref="AssetPropertyGraph.FindTarget(IGraphNode, IGraphNode)"/> when linking, to allow custom
    ///   links for cases such as <see cref="AssetComposite"/>.
    /// </remarks>
    public class AssetToBaseNodeLinker : AssetGraphNodeLinker
    {
        private readonly AssetPropertyGraph propertyGraph;

        public AssetToBaseNodeLinker([NotNull] AssetPropertyGraph propertyGraph)
            : base(propertyGraph.Definition)
        {
            this.propertyGraph = propertyGraph;
        }

        protected override IGraphNode FindTarget(IGraphNode sourceNode)
        {
            var defaultTarget = base.FindTarget(sourceNode);
            return propertyGraph.FindTarget(sourceNode, defaultTarget);
        }

        public override ObjectReference FindTargetReference(IGraphNode sourceNode, IGraphNode targetNode, ObjectReference sourceReference)
        {
            // Not identifiable - default applies
            if (sourceReference.Index.IsEmpty || sourceReference.ObjectValue is null)
                return base.FindTargetReference(sourceNode, targetNode, sourceReference);

            // Special case for objects that are identifiable: the object must be linked to the base only if it has the same id
            var sourceAssetNode = (AssetObjectNode) sourceNode;
            var targetAssetNode = (AssetObjectNode) targetNode;
            if (!CollectionItemIdHelper.HasCollectionItemIds(sourceAssetNode.Retrieve()))
                return null;

            // Enumerable reference: we look for an object with the same id
            var targetReference = targetAssetNode.ItemReferences;
            var sourceIds = CollectionItemIdHelper.GetCollectionItemIds(sourceNode.Retrieve());
            var targetIds = CollectionItemIdHelper.GetCollectionItemIds(targetNode.Retrieve());
            var itemId = sourceIds[sourceReference.Index.Value];
            var targetKey = targetIds.GetKey(itemId);
            foreach (var targetRef in targetReference)
            {
                if (Equals(targetRef.Index.Value, targetKey))
                    return targetRef;
            }

            return null;
        }
    }
}
