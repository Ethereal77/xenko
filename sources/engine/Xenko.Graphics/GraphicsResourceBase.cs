// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

using Xenko.Core;

namespace Xenko.Graphics
{
    public partial class GraphicsResourceBase : ComponentBase
    {
        internal GraphicsResourceLifetimeState LifetimeState;
        public Action<GraphicsResourceBase> Reload;

        /// <summary>
        /// Gets the graphics device attached to this instance.
        /// </summary>
        /// <value>The graphics device.</value>
        public GraphicsDevice GraphicsDevice
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsResourceBase"/> class.
        /// </summary>
        protected GraphicsResourceBase()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsResourceBase"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        protected GraphicsResourceBase(GraphicsDevice device) : this(device, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsResourceBase"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="name">The name.</param>
        protected GraphicsResourceBase(GraphicsDevice device, string name) : base(name)
        {
            AttachToGraphicsDevice(device);
        }

        internal void AttachToGraphicsDevice(GraphicsDevice device)
        {
            GraphicsDevice = device;

            if (device != null)
            {
                // Add GraphicsResourceBase to device resources
                var resources = device.Resources;
                lock (resources)
                {
                    resources.Add(this);
                }
            }

            Initialize();
        }

        /// <summary>
        /// Called when graphics device is inactive (put in the background and rendering is paused).
        /// It should voluntarily release objects that can be easily recreated, such as FBO and dynamic buffers.
        /// </summary>
        /// <returns>True if item transitioned to a <see cref="GraphicsResourceLifetimeState.Paused"/> state.</returns>
        protected internal virtual bool OnPause()
        {
            return false;
        }

        /// <summary>
        /// Called when graphics device is resumed from either paused or destroyed state.
        /// If possible, resource should be recreated.
        /// </summary>
        protected internal virtual void OnResume()
        {
        }

        /// <inheritdoc/>
        protected override void Destroy()
        {
            var device = GraphicsDevice;

            if (device != null)
            {
                // Remove GraphicsResourceBase from device resources
                var resources = device.Resources;
                lock (resources)
                {
                    resources.Remove(this);
                }
                if (LifetimeState != GraphicsResourceLifetimeState.Destroyed)
                {
                    OnDestroyed();
                    LifetimeState = GraphicsResourceLifetimeState.Destroyed;
                }
            }

            // No need for reload anymore, allow it to be GC
            Reload = null;

            base.Destroy();
        }
    }
}
