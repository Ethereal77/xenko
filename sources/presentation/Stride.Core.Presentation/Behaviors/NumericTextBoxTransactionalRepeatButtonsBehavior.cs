// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Windows;

using Microsoft.Xaml.Behaviors;

using Stride.Core.Transactions;
using Stride.Core.Presentation.Controls;
using Stride.Core.Presentation.Services;

namespace Stride.Core.Presentation.Behaviors
{
    /// <summary>
    /// This behavior allows more convenient editing of the value of a char using a TextBox.
    /// </summary>
    public class NumericTextBoxTransactionalRepeatButtonsBehavior : Behavior<NumericTextBox>
    {
        private ITransaction transaction;

        public static DependencyProperty UndoRedoServiceProperty = DependencyProperty.Register(nameof(UndoRedoService), typeof(IUndoRedoService), typeof(NumericTextBoxTransactionalRepeatButtonsBehavior));

        public IUndoRedoService UndoRedoService { get { return (IUndoRedoService)GetValue(UndoRedoServiceProperty); } set { SetValue(UndoRedoServiceProperty, value); } }

        protected override void OnAttached()
        {
            AssociatedObject.RepeatButtonPressed += RepeatButtonPressed;
            AssociatedObject.RepeatButtonReleased += RepeatButtonReleased;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.RepeatButtonPressed -= RepeatButtonPressed;
            AssociatedObject.RepeatButtonReleased -= RepeatButtonReleased;
        }

        private void RepeatButtonPressed(object sender, RepeatButtonPressedRoutedEventArgs e)
        {
            transaction = UndoRedoService?.CreateTransaction();
        }

        private void RepeatButtonReleased(object sender, RepeatButtonPressedRoutedEventArgs e)
        {
            transaction?.Continue();
            transaction?.Complete();
            transaction = null;
        }
    }
}
