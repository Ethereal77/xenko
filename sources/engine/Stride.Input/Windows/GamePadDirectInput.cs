// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

#if STRIDE_UI_WINFORMS || STRIDE_UI_WPF

using System;

namespace Stride.Input
{
    /// <summary>
    /// A known gamepad that uses DirectInput as a driver
    /// </summary>
    internal class GamePadDirectInput : GamePadFromLayout, IGamePadDevice, IDisposable
    {
        private GameControllerDirectInput controller;

        public GamePadDirectInput(InputSourceWindowsDirectInput source, InputManager inputManager, GameControllerDirectInput controller, GamePadLayout layout)
            : base(inputManager, controller, layout)
        {
            this.controller = controller;
            Source = source;
            Name = controller.Name;
            Id = controller.Id;
            ProductId = controller.ProductId;
        }

        public void Dispose()
        {
            controller.Dispose();
        }

        public new int Index
        {
            get => base.Index;
            set => SetIndexInternal(newIndex: value, isDeviceSideChange: false);
        }

        public override string Name { get; }

        public override Guid Id { get; }

        public override Guid ProductId { get; }

        public override IInputSource Source { get; }

        public override void SetVibration(float smallLeft, float smallRight, float largeLeft, float largeRight)
        {
            // No vibration support in DirectInput gamepads
        }
    }
}

#endif
