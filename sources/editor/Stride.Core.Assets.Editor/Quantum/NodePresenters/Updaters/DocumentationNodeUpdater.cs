// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Core.Assets.Editor.Quantum.NodePresenters.Keys;
using Stride.Core.Assets.Editor.Services;
using Stride.Core;
using Stride.Core.Annotations;
using Stride.Core.Presentation.Quantum.Presenters;

namespace Stride.Core.Assets.Editor.Quantum.NodePresenters.Updaters
{
    public sealed class DocumentationNodeUpdater : AssetNodePresenterUpdaterBase
    {
        private readonly UserDocumentationService documentationService;

        public DocumentationNodeUpdater([NotNull] UserDocumentationService documentationService)
        {
            this.documentationService = documentationService ?? throw new ArgumentNullException(nameof(documentationService));
        }

        protected override void UpdateNode(IAssetNodePresenter node)
        {
            if (!(node is MemberNodePresenter memberNode))
                return;

            if (node.Index.Value is PropertyKey propertyKey)
            {
                var propertyKeyDocumentation = documentationService.GetPropertyKeyDocumentation(propertyKey);
                if (propertyKeyDocumentation != null)
                    node.AttachedProperties.Add(DocumentationData.Key, propertyKeyDocumentation);
            }
            else
            {
                var memberDocumentation = documentationService.GetMemberDocumentation(memberNode.MemberDescriptor, node.Root.Type);
                if (memberDocumentation != null)
                {
                    node.AttachedProperties.Add(DocumentationData.Key, memberDocumentation);
                }
            }
        }
    }
}
