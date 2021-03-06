// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// See the LICENSE.md file in the project root for full license information.

namespace Stride.Graphics
{
    /// <summary>
    /// Defines how a view is selected from a resource.
    /// </summary>
    /// <remarks>
    /// This selection model is taken from Nuaj by Patapom (http://wiki.patapom.com/index.php/Nuaj)
    /// </remarks>
    public enum ViewType
    {
        /// <summary>
        /// Gets a texture view for the whole texture for all mips/arrays dimensions.
        /// </summary>
        /// <example>Here is what the view covers with whatever mipLevelIndex/arrayIndex
        /// 
        ///        Array0 Array1 Array2
        ///       ______________________
        ///  Mip0 |   X  |   X  |   X  |
        ///       |------+------+------|
        ///  Mip1 |   X  |   X  |   X  |
        ///       |------+------+------|
        ///  Mip2 |   X  |   X  |   X  |
        ///       ----------------------
        /// </example>
        Full = 0,

        /// <summary>
        /// Gets a single texture view at the specified index in the mip hierarchy and in the array of textures
        /// The texture view contains a single texture element at the specified mip level and array index
        /// </summary>
        /// <example>Here is what the view covers with mipLevelIndex=1 and mrrayIndex=1
        /// 
        ///        Array0 Array1 Array2
        ///       ______________________
        ///  Mip0 |      |      |      |
        ///       |------+------+------|
        ///  Mip1 |      |  X   |      |
        ///       |------+------+------|
        ///  Mip2 |      |      |      |
        ///       ----------------------
        /// </example>
        Single = 1,

        /// <summary>
        /// Gets a band texture view at the specified index in the mip hierarchy and in the array of textures
        /// The texture view contains all the mip level texture elements from the specified mip level and array index
        /// </summary>
        /// <example>Here is what the view covers with mipLevelIndex=1 and mrrayIndex=1
        /// 
        ///        Array0 Array1 Array2
        ///       ______________________
        ///  Mip0 |      |      |      |
        ///       |------+------+------|
        ///  Mip1 |      |  X   |      |
        ///       |------+------+------|
        ///  Mip2 |      |  X   |      |
        ///       ----------------------
        /// </example>
        ArrayBand = 2,

        /// <summary>
        /// Gets a band texture view at the specified index in the mip hierarchy and in the array of textures
        /// The texture view contains all the array texture elements from the specified mip level and array index
        /// </summary>
        /// <example>Here is what the view covers with mipLevelIndex=1 and mrrayIndex=1
        /// 
        ///        Array0 Array1 Array2
        ///       ______________________
        ///  Mip0 |      |      |      |
        ///       |------+------+------|
        ///  Mip1 |      |  X   |  X   |
        ///       |------+------+------|
        ///  Mip2 |      |      |      |
        ///       ----------------------
        /// </example>
        MipBand = 3,
    }
}
