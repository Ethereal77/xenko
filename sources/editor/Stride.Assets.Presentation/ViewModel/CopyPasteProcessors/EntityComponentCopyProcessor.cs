// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Linq;

using Stride.Core.Assets.Editor.Services;
using Stride.Core.Assets.Yaml;
using Stride.Core.Annotations;
using Stride.Engine;

namespace Stride.Assets.Presentation.ViewModel.CopyPasteProcessors
{
    public class EntityComponentCopyProcessor : ICopyProcessor
    {
        /// <inheritdoc />
        public bool Accept(Type dataType)
        {
            return dataType == typeof(TransformComponent) || dataType == typeof(EntityComponentCollection);
        }

        /// <inheritdoc />
        public bool Process(ref object data, AttachedYamlAssetMetadata metadata)
        {
            if (data is TransformComponent transform)
            {
                PatchTransformComponent(transform);
                return true;
            }

            if (data is EntityComponentCollection collection)
            {
                var processed = false;
                foreach (var t in collection.OfType<TransformComponent>())
                {
                    PatchTransformComponent(t);
                    processed = true;
                }
                return processed;
            }

            return false;
        }

        private static void PatchTransformComponent([NotNull] TransformComponent transform)
        {
            // We don't want to copy the children of a transform component
            transform.Children.Clear();
        }
    }
}
