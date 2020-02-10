// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core.Diagnostics;

namespace Xenko.Core.Assets
{
    /// <summary>
    /// Result returned when loading a session using <see cref="PackageSession.Load(string,PackageSessionResult,System.Nullable{System.Threading.CancellationToken},bool)"/>
    /// </summary>
    public sealed class PackageSessionResult : LoggerResult
    {
        /// <summary>
        /// Gets or sets the loaded session.
        /// </summary>
        /// <value>The session.</value>
        public PackageSession Session { get; internal set; }

        /// <summary>
        /// Gets or sets whether the operation has been cancelled by user.
        /// </summary>
        public bool OperationCancelled { get; set; }

        /// <inheritdoc/>
        public override void Clear()
        {
            base.Clear();
            Session = null;
        }
    }
}
