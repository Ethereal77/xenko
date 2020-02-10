// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

using Xenko.Core;

namespace Xenko.Core.Assets.Compiler
{
    /// <summary>
    /// The context used when compiling an asset in a Package.
    /// </summary>
    public class AssetCompilerContext : CompilerContext
    {
        /// <summary>
        /// Gets or sets the name of the profile being built.
        /// </summary>
        public string Profile { get; set; }

        /// <summary>
        /// Gets or sets the build configuration (Debug, Release, AppStore, Testing)
        /// </summary>
        public string BuildConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the entry package this build was called upon.
        /// </summary>
        public Package Package { get; set; }

        /// <summary>
        /// Gets or sets the target platform for compiler is being used for.
        /// </summary>
        /// <value>The platform.</value>
        public PlatformType Platform { get; set; }

        /// <summary>
        /// The compilation context type of this compiler context
        /// </summary>
        public Type CompilationContext;
    }
}
