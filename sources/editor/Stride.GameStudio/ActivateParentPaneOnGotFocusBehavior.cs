// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Windows;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

using Xceed.Wpf.AvalonDock.Controls;

using Stride.Core.Presentation.Extensions;

namespace Stride.GameStudio
{
    // TODO: this behavior was previously broken, it might work now (migration to AvalonDock) but has not been tested!
    public class ActivateParentPaneOnGotFocusBehavior : Behavior<Control>
    {
        protected override void OnAttached()
        {
            AssociatedObject.GotKeyboardFocus += GotFocus;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotKeyboardFocus -= GotFocus;
        }

        private void GotFocus(object sender, RoutedEventArgs e)
        {
            var pane = AssociatedObject.FindVisualParentOfType<LayoutAnchorableControl>();
            if (pane != null)
            {
                pane.Model.IsActive = true;
            }
        }
    }
}
