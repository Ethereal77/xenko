// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Stride.Core.Presentation.View
{
    /// <summary>
    /// A default implementation of the <see cref="ITemplateProvider"/> interface that matches any object.
    /// </summary>
    public class DefaultTemplateProvider : TemplateProviderBase
    {
        /// <inheritdoc/>
        public override string Name => "Default";

        /// <inheritdoc/>
        public override bool Match(object obj)
        {
            return true;
        }
    }
}
