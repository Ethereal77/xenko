// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

namespace Stride.Core.Assets
{
    public class PackageSaveParameters
    {
        private static readonly PackageSaveParameters DefaultParameters = new PackageSaveParameters();

        public static PackageSaveParameters Default()
        {
            return DefaultParameters.Clone();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>PackageLoadParameters.</returns>
        public PackageSaveParameters Clone()
        {
            return (PackageSaveParameters)MemberwiseClone();
        }

        public Func<AssetItem, bool> AssetFilter;
    }
}
