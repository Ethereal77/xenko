// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FreeImageAPI
{
	/// <summary>
	/// Provides methods for working with generic bitmap scanlines.
	/// </summary>
	/// <typeparam name="T">Type of the bitmaps' scanlines.</typeparam>
	public sealed class Scanline<T> : MemoryArray<T> where T : struct
	{
		/// <summary>
		/// Initializes a new instance based on the specified FreeImage bitmap.
		/// </summary>
		/// <param name="dib">Handle to a FreeImage bitmap.</param>
		public Scanline(FIBITMAP dib)
			: this(dib, 0)
		{
		}

		/// <summary>
		/// Initializes a new instance based on the specified FreeImage bitmap.
		/// </summary>
		/// <param name="dib">Handle to a FreeImage bitmap.</param>
		/// <param name="scanline">Index of the zero based scanline.</param>
		public Scanline(FIBITMAP dib, int scanline)
			: this(dib, scanline, (int)(typeof(T) == typeof(FI1BIT) ?
				FreeImage.GetBPP(dib) * FreeImage.GetWidth(dib) :
				typeof(T) == typeof(FI4BIT) ?
				FreeImage.GetBPP(dib) * FreeImage.GetWidth(dib) / 4 :
				(FreeImage.GetBPP(dib) * FreeImage.GetWidth(dib)) / (Marshal.SizeOf(typeof(T)) * 8)))
		{
		}

		internal Scanline(FIBITMAP dib, int scanline, int length)
			: base(FreeImage.GetScanLine(dib, scanline), length)
		{
			if (dib.IsNull)
			{
				throw new ArgumentNullException("dib");
			}
			if ((scanline < 0) || (scanline >= FreeImage.GetHeight(dib)))
			{
				throw new ArgumentOutOfRangeException("scanline");
			}
		}
	}
}
