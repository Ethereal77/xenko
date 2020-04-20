// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Threading.Tasks;

using Stride.Core.Assets.Editor.ViewModel;
using Stride.Core.Diagnostics;

namespace Stride.GameStudio.Debugging
{
    /// <summary>
    /// An interface that can run a game in debug mode, inspect states and propagate live changes
    /// </summary>
    public interface IDebugService : IDisposable
    {
        Task<bool> StartDebug(EditorViewModel editor, ProjectViewModel currentProject, LoggerResult logger);
    }
}
