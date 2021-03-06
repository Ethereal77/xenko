// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;

using Stride.Core.Assets;

namespace Stride.Assets.Media
{
    public class RawVideoAssetImporter : RawAssetImporterBase<VideoAsset>
    {
        // Supported file extensions for this importer
        public const string FileExtensions = ".avi,.mkv,.mov,.mp4";
        private static readonly Guid Uid = new Guid("48b6a448-08fd-4db3-bef7-82b9b8b19437");

        /// <inheritdoc />
        public override Guid Id => Uid;

        /// <inheritdoc />
        public override string Description => "Raw video importer for creating Video assets";

        /// <inheritdoc />
        public override string SupportedFileExtensions => FileExtensions;
    }
}
