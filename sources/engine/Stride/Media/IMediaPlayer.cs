// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

namespace Stride.Media
{
    /// <summary>
    /// Interface for playable media
    /// </summary>
    public interface IMediaPlayer : IMediaReader
    {
        /// <summary>
        /// Start or resume playing the media.
        /// </summary>
        /// <remarks>A call to Play when the media is already playing has no effects.</remarks>
        void Play();

        /// <summary>
        /// Pause the media.
        /// </summary>
        /// <remarks>A call to Pause when the media is already paused or stopped has no effects.</remarks>
        void Pause();

        /// <summary>
        /// Stop playing the media immediately and reset the media to the beginning of the source.
        /// </summary>
        /// <remarks>A call to Stop when the media is already stopped has no effects</remarks>
        void Stop();
    }
}
