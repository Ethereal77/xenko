// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

namespace Stride.Input
{
    public enum TextInputEventType
    {
        /// <summary>
        /// When new text is entered
        /// </summary>
        Input,

        /// <summary>
        /// This the current text that has not yet been entered but is still being edited
        /// </summary>
        Composition,
    }
}