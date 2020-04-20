// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Windows;

namespace Stride.Core.Presentation.Core
{
    public class CancelRoutedEventArgs : RoutedEventArgs
    {
        public bool Cancel { get; set; }

        public CancelRoutedEventArgs(RoutedEvent routedEvent, bool cancel = false)
            : base(routedEvent)
        {
            Cancel = cancel;
        }
    }

    public delegate void CancelRoutedEventHandler(object sender, CancelRoutedEventArgs e);
}
