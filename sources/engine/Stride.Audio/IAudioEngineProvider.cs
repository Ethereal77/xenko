// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Stride.Audio
{
    /// <summary>
    /// Used internally to find the currently active audio engine 
    /// </summary>
    public interface IAudioEngineProvider
    {
        AudioEngine AudioEngine { get; }
    }
}
