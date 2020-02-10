// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core;

namespace Xenko.Rendering.Materials.ComputeColors
{
    /// <summary>
    /// A shader outputing a single scalar value.
    /// </summary>
    [DataContract("ComputeShaderClassColor")]
    [Display("Shader")]
    public class ComputeShaderClassColor : ComputeShaderClassBase<IComputeColor>, IComputeColor
    {
        private int hashCode = 0;

        /// <inheritdoc/>
        public bool HasChanged
        {
            get
            {
                if (hashCode != 0 && hashCode == (MixinReference?.GetHashCode() ?? 0))
                    return false;

                hashCode = (MixinReference?.GetHashCode() ?? 0);
                return true;
            }
        }
    }
}
