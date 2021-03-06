// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

namespace Stride.Core.BuildEngine
{
    /// <summary>
    /// An implementation of the <see cref="IBuildStepProvider"/> interface that allows to create a build step provider
    /// from an anonymous function.
    /// </summary>
    public class AnonymousBuildStepProvider : IBuildStepProvider
    {
        private readonly Func<int, BuildStep> providerFunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousBuildStepProvider"/> class.
        /// </summary>
        /// <param name="providerFunction">The function that provides build steps.</param>
        public AnonymousBuildStepProvider(Func<int, BuildStep> providerFunction)
        {
            if (providerFunction == null) throw new ArgumentNullException("providerFunction");
            this.providerFunction = providerFunction;
        }

        /// <inheritdoc/>
        public BuildStep GetNextBuildStep(int maxPriority)
        {
            return providerFunction(maxPriority);
        }
    }
}
