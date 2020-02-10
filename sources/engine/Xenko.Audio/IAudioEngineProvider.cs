// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Xenko.Audio
{
    /// <summary>
    /// Used internally to find the currently active audio engine 
    /// </summary>
    public interface IAudioEngineProvider
    {
        AudioEngine AudioEngine { get; }
    }
}
