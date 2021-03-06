// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

namespace Stride.Core.BuildEngine
{
    /// <summary>
    /// This interface describes a class that is capable of providing build steps to a <see cref="DynamicBuildStep"/>.
    /// </summary>
    public interface IBuildStepProvider
    {
        /// <summary>
        /// Gets the next build step to execute.
        /// </summary>
        /// <returns>The next build step to execute, or <c>null</c> if there is no build step to execute.</returns>
        BuildStep GetNextBuildStep(int maxPriority);
    }
}
