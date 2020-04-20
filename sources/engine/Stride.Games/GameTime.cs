// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace Stride.Games
{
    /// <summary>
    /// Current timing used for variable-step (real time) or fixed-step (game time) games.
    /// </summary>
    public class GameTime
    {
        private TimeSpan accumulatedElapsedTime;
        private int accumulatedFrameCountPerSecond;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameTime" /> class.
        /// </summary>
        public GameTime()
        {
            accumulatedElapsedTime = TimeSpan.Zero;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameTime" /> class.
        /// </summary>
        /// <param name="totalTime">The total game time since the start of the game.</param>
        /// <param name="elapsedTime">The elapsed game time since the last update.</param>
        public GameTime(TimeSpan totalTime, TimeSpan elapsedTime)
        {
            Total = totalTime;
            Elapsed = elapsedTime;
            accumulatedElapsedTime = TimeSpan.Zero;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the elapsed game time since the last update
        /// </summary>
        /// <value>The elapsed game time.</value>
        public TimeSpan Elapsed { get; private set; }

        /// <summary>
        /// Gets the amount of game time since the start of the game.
        /// </summary>
        /// <value>The total game time.</value>
        public TimeSpan Total { get; private set; }

        /// <summary>
        /// Gets the current frame count since the start of the game.
        /// </summary>
        public int FrameCount { get; private set; }

        /// <summary>
        /// Gets the number of frame per second (FPS) for the current running game.
        /// </summary>
        /// <value>The frame per second.</value>
        public float FramePerSecond { get; private set; }

        /// <summary>
        /// Gets the time per frame.
        /// </summary>
        /// <value>The time per frame.</value>
        public TimeSpan TimePerFrame { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="FramePerSecond"/> and <see cref="TimePerFrame"/> were updated for this frame.
        /// </summary>
        /// <value><c>true</c> if the <see cref="FramePerSecond"/> and <see cref="TimePerFrame"/> were updated for this frame; otherwise, <c>false</c>.</value>
        public bool FramePerSecondUpdated { get; private set; }

        internal void Update(TimeSpan totalGameTime, TimeSpan elapsedGameTime, bool incrementFrameCount)
        {
            Total = totalGameTime;
            Elapsed = elapsedGameTime;
            FramePerSecondUpdated = false;

            if (incrementFrameCount)
            {
                accumulatedElapsedTime += elapsedGameTime;
                var accumulatedElapsedGameTimeInSecond = accumulatedElapsedTime.TotalSeconds;
                if (accumulatedFrameCountPerSecond > 0 && accumulatedElapsedGameTimeInSecond > 1.0)
                {
                    TimePerFrame = TimeSpan.FromTicks(accumulatedElapsedTime.Ticks / accumulatedFrameCountPerSecond);
                    FramePerSecond = (float)(accumulatedFrameCountPerSecond / accumulatedElapsedGameTimeInSecond);
                    accumulatedFrameCountPerSecond = 0;
                    accumulatedElapsedTime = TimeSpan.Zero;
                    FramePerSecondUpdated = true;
                }

                accumulatedFrameCountPerSecond++;
                FrameCount++;
            }
        }

        internal void Reset(TimeSpan totalGameTime)
        {
            Update(totalGameTime, TimeSpan.Zero, false);
            accumulatedElapsedTime = TimeSpan.Zero;
            accumulatedFrameCountPerSecond = 0;
            FrameCount = 0;
        }

        #endregion
    }
}
