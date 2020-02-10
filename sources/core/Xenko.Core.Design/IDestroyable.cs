// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Xenko.Core
{
    /// <summary>
    /// Similar to the <see cref="System.IDisposable"/> but only deals with managed resources.
    /// </summary>
    /// <remarks>
    /// Class implementing both <see cref="IDestroyable"/> and <see cref="System.IDisposable"/> should call <see cref="Destroy"/>
    /// from the <see cref="System.IDisposable.Dispose"/> method when appropriate.
    /// </remarks>
    public interface IDestroyable
    {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting managed resources.
        /// </summary>
        void Destroy();
    }
}
