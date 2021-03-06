// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core;

namespace Stride.Graphics
{
    /// <summary>
    /// This class represents a graphics adapter.
    /// </summary>
    public sealed partial class GraphicsAdapter : ComponentBase
    {
        private readonly GraphicsOutput[] outputs;

        /// <summary>
        /// Gets the <see cref="GraphicsOutput"/> attached to this adapter
        /// </summary>
        /// <returns>The <see cref="GraphicsOutput"/> attached to this adapter.</returns>
        public GraphicsOutput[] Outputs
        {
            get
            {
                return outputs;
            }
        }

        /// <summary>
        /// Return the description of this adapter
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Description;
        }

        /// <summary>
        /// The unique id in the form of string of this device
        /// </summary>
        public string AdapterUid { get; internal set; }
    }
}
