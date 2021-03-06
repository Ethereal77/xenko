﻿// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;

using Stride.Core.Assets.Quantum.Visitors;
using Stride.Core.Annotations;
using Stride.Core.Quantum;

namespace Stride.Core.Assets.Quantum
{
    /// <summary>
    /// An implementation of <see cref="GraphNodeChangeListener"/> that uses an <see cref="AssetGraphVisitorBase"/> to prevent visiting object references.
    /// </summary>
    public class AssetGraphNodeChangeListener : GraphNodeChangeListener
    {
        [NotNull] private readonly AssetPropertyGraphDefinition propertyGraphDefinition;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetGraphNodeChangeListener"/> class.
        /// </summary>
        /// <param name="rootNode">The root node of the extended graph to listen to.</param>
        /// <param name="propertyGraphDefinition">The <see cref="AssetPropertyGraphDefinition"/> that describes which nodes represent an object reference.</param>
        public AssetGraphNodeChangeListener(IGraphNode rootNode, [NotNull] AssetPropertyGraphDefinition propertyGraphDefinition)
            : base(rootNode)
        {
            this.propertyGraphDefinition = propertyGraphDefinition ?? throw new ArgumentNullException(nameof(propertyGraphDefinition));
        }

        /// <inheritdoc/>
        protected override GraphVisitorBase CreateVisitor()
        {
            return new AssetGraphVisitorBase(propertyGraphDefinition);
        }
    }
}
