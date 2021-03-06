// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Linq;
using System.Windows;

namespace Stride.Core.Presentation.Graph
{
    /// <summary>
    /// 
    /// </summary>
    public class LinkSelectedEventArgs : EventArgs
    {
        public FrameworkElement Link { get; private set; }

        public LinkSelectedEventArgs(FrameworkElement link)
            : base()
        {
            Link = link;
        }
    }

    public delegate void LinkSelectedEventHandler(object sender, LinkSelectedEventArgs args);
}
