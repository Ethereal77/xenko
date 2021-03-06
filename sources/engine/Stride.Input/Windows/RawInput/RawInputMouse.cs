// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// See the LICENSE.md file in the project root for full license information.

#if STRIDE_UI_WINFORMS || STRIDE_UI_WPF

using System;
using System.Windows.Forms;

namespace Stride.Input.RawInput
{
    internal class RawInputMouse : IDisposable
    {
        private const ushort MOUSE_MOVE_ABSOLUTE_BIT = 1;

        private readonly IntPtr targetWindow;

        public event EventHandler<RawInputMouseEventArgs> events;

        private readonly RawInputMessageFilter messageFilter;
        private bool disposedValue;

        public RawInputMouse(IntPtr targetWindow)
        {
            this.targetWindow = targetWindow;
            messageFilter = new RawInputMessageFilter(this.HandleInputMessage, null, null);
        }

        public void Start()
        {
            var result = RawInput.RegisterDevice(UsagePage.HID_USAGE_PAGE_GENERIC, UsageId.HID_USAGE_GENERIC_MOUSE, ModeFlags.RIDEV_INPUTSINK, targetWindow);
            if (!result)
                throw new Exception("Failed to register a mouse device for raw input.");

            Application.AddMessageFilter(messageFilter);
        }

        public void End()
        {
            Application.RemoveMessageFilter(messageFilter);

            var result = RawInput.RegisterDevice(UsagePage.HID_USAGE_PAGE_GENERIC, UsageId.HID_USAGE_GENERIC_MOUSE, ModeFlags.RIDEV_REMOVE, IntPtr.Zero);
            if (!result)
                throw new Exception("Failed to unregister a mouse device for raw input.");
        }

        public void HandleInputMessage(RawInput.RawMouse mouseInput)
        {
            // TODO: Handle absolute pointer positions. These events can happen with drawing tablets or RDP for example
            events?.Invoke(null, new RawInputMouseEventArgs
            {
                isRelative = (mouseInput.Flags & MOUSE_MOVE_ABSOLUTE_BIT) == 0,
                X = mouseInput.LastX,
                Y = mouseInput.LastY
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                End();

                disposedValue = true;
            }
        }

        ~RawInputMouse()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

#endif
