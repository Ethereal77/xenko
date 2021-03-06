// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

namespace Stride.Core
{
    /// <summary>
    /// This class is an implementation of the <see cref="IDisposable"/> interface that does nothing when disposed.
    /// </summary>
    public class NullDisposable : IDisposable
    {
        /// <summary>
        /// A static instance of the <see cref="NullDisposable"/> class.
        /// </summary>
        public static readonly NullDisposable Instance = new NullDisposable();

        /// <summary>
        /// Implementation of the <see cref="IDisposable.Dispose"/> method. This method does nothing.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
