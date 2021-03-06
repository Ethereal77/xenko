// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

namespace Stride.Core.Serialization.Contents
{
    /// <summary>
    /// A subtype of <see cref="Exception"/> thrown by the <see cref="ContentManager"/>.
    /// </summary>
    internal class ContentManagerException : Exception
    {
        public ContentManagerException(string message) : base(message)
        {
        }

        public ContentManagerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
