// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Core.BuildEngine;
using Stride.Core.Diagnostics;

namespace Stride.Core.Assets.Compiler
{
    /// <summary>
    /// Result of a compilation of assets when using <see cref="IAssetCompiler.Prepare"/>
    /// </summary>
    public class AssetCompilerResult : LoggerResult
    {
        private ListBuildStep buildSteps;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerResult" /> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        public AssetCompilerResult(string moduleName = null) : base(moduleName)
        {
            BuildSteps = new ListBuildStep();
        }

        /// <summary>
        /// Gets or sets the build steps generated for the build engine. This can be null if <see cref="LoggerResult.HasErrors"/> is true.
        /// </summary>
        /// <value>The build step.</value>
        public ListBuildStep BuildSteps { get { return buildSteps; } set { if (value == null) throw new ArgumentNullException("value", @"The BuildSteps property cannot be set to null"); buildSteps = value; } }
    }
}
