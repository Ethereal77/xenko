// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Native;

namespace Stride.Audio
{
    /// <summary>
    /// Represents a 3D audio listener in the audio scene.
    /// This object, used in combination with an <see cref="AudioEmitter"/>, can simulate 3D audio localization effects for a sound implemention the <see cref="IPositionableSound"/> interface.
    /// For more details take a look at the <see cref="IPositionableSound.Apply3D"/> function.
    /// </summary>
    /// <seealso cref="IPositionableSound.Apply3D"/>
    /// <seealso cref="AudioEmitter"/>
    public class AudioListener : IDisposable
    {
        public AudioListener(AudioEngine engine)
        {
            if (engine.State == AudioEngineState.Invalidated) return;

            Listener = AudioLayer.ListenerCreate(engine.AudioDevice);
            AudioLayer.ListenerEnable(Listener);
        }

        /// <summary>
        /// The position of the listener in the 3D world.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// The velocity of the listener in the 3D world.
        /// </summary>
        /// <remarks>This is only used to calculate the doppler effect on the sound effect</remarks>
        public Vector3 Velocity;

        private Vector3 up;

        /// <summary>
        /// Gets or sets the Up orientation vector for this listener. This vector up of the world for the listener.
        /// </summary>
        /// <remarks>
        /// <para>By default, this value is (0,1,0).</para>
        /// <para>The value provided will be normalized if it is not already.</para>
        /// <para>The values of the Forward and Up vectors must be orthonormal (at right angles to one another).
        /// Behavior is undefined if these vectors are not orthonormal.</para>
        /// <para>Doppler and Matrix values between an <see name="AudioEmitter"/> and an <see cref="AudioListener"/> are effected by the listener orientation.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">The value provided to the set accessor is (0,0,0).</exception>
        public Vector3 Up
        {
            get
            {
                return up;
            }
            set
            {
                if (value == Vector3.Zero)
                    throw new InvalidOperationException("The value of the Up vector can not be (0,0,0)");

                up = Vector3.Normalize(value);
            }
        }

        private Vector3 forward;

        /// <summary>
        /// Gets or sets the forward orientation vector for this listener. This vector represents the orientation the listener is looking at.
        /// </summary>
        /// <remarks>
        /// <para>By default, this value is (0,0,1).</para>
        /// <para>The value provided will be normalized if it is not already.</para>
        /// <para>The values of the Forward and Up vectors must be orthonormal (at right angles to one another).
        /// Behavior is undefined if these vectors are not orthonormal.</para>
        /// <para>Doppler and Matrix values between an <see name="AudioEmitter"/> and an <see cref="AudioListener"/> are effected by the listener orientation.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">The value provided to the set accessor is (0,0,0) or <see cref="Up"/>.</exception>
        public Vector3 Forward
        {
            get
            {
                return forward;
            }
            set
            {
                if (value == Vector3.Zero)
                    throw new InvalidOperationException("The value of the Forward vector can not be (0,0,0)");

                forward = Vector3.Normalize(value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioListener"/> class.
        /// </summary>
        public AudioListener()
        {
            Forward = new Vector3(0, 0, 1);
            Up = new Vector3(0, 1, 0);
        }

        /// <summary>
        /// Internal object that represents a device context actually, this is to allow multiple listeners.
        /// </summary>
        internal AudioLayer.Listener Listener;

        internal Matrix WorldTransform;

        /// <summary>
        /// Disposes the Listener
        /// </summary>
        public void Dispose()
        {
            if (Listener.Ptr == IntPtr.Zero) return;

            AudioLayer.ListenerDisable(Listener);
            AudioLayer.ListenerDestroy(Listener);
        }

        internal void Update()
        {
            if (Listener.Ptr == IntPtr.Zero) return;
            AudioLayer.ListenerPush3D(Listener, ref Position, ref forward, ref up, ref Velocity, ref WorldTransform);
        }
    }
}
