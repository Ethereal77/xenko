// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stride.TextureConverter.Requests
{
    /// <summary>
    /// Request to flip a texture vertically or horizontally
    /// </summary>
    internal class FlippingRequest : IRequest
    {

        public override RequestType Type { get { return RequestType.Flipping; } }


        /// <summary>
        /// The requested orientation flip
        /// </summary>
        public Orientation Flip { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="FlippingRequest"/> class.
        /// </summary>
        /// <param name="orientation">The orientation.</param>
        public FlippingRequest(Orientation orientation)
        {
            this.Flip = orientation;
        }

    }
}
