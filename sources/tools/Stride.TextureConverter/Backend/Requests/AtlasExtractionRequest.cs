// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Stride.TextureConverter.Requests
{
    /// <summary>
    /// Request to extract one or every textures from an atlas.
    /// </summary>
    class AtlasExtractionRequest : IRequest
    {
        public override RequestType Type { get { return RequestType.AtlasExtraction; } }

        /// <summary>
        /// The name of the texture to extract
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// The minimum size of the smallest mipmap.
        /// </summary>
        public int MinimumMipMapSize { get; private set; }

        /// <summary>
        /// The extracted texture. 
        /// </summary>
        public TexImage Texture { get; set; }

        /// <summary>
        /// The extracted texture list.
        /// </summary>
        public List<TexImage> Textures { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="AtlasExtractionRequest"/> class. Used to extract a single texture.
        /// </summary>
        /// <param name="name">The name.</param>
        public AtlasExtractionRequest(string name, int minimimMipmapSize)
        {
            Name = name;
            MinimumMipMapSize = minimimMipmapSize;
            Texture = new TexImage();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AtlasExtractionRequest"/> class. Used to extract every textures.
        /// </summary>
        public AtlasExtractionRequest(int minimimMipmapSize)
        {
            MinimumMipMapSize = minimimMipmapSize;
            Textures = new List<TexImage>();
        }
    }
}
