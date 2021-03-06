// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Collections.Generic;
using System.ComponentModel;

using Stride.Core;

namespace Stride.Rendering.Materials
{
    /// <summary>
    /// A composition material to blend different materials in a stack based manner.
    /// </summary>
    [DataContract("MaterialBlendLayers")]
    [Display("Material Layers")]
    public class MaterialBlendLayers : List<MaterialBlendLayer>, IMaterialLayers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialBlendLayers"/> class.
        /// </summary>
        public MaterialBlendLayers()
        {
            Enabled = true;
        }

        [DataMemberIgnore]
        [DefaultValue(true)]
        public bool Enabled { get; set; }

        public virtual void Visit(MaterialGeneratorContext context)
        {
            if (!Enabled)
                return;

            foreach (var layer in this)
            {
                layer.Visit(context);
            }
        }
    }
}
