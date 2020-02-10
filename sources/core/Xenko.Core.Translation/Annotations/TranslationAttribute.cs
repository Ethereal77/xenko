// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

using Xenko.Core.Annotations;

namespace Xenko.Core.Translation.Annotations
{
    /// <summary>
    /// Specifies a translatable name, with support for context and plural.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum |
                    AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
    public class TranslationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationAttribute"/> class.
        /// </summary>
        public TranslationAttribute([NotNull] string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationAttribute"/> class.
        /// </summary>
        public TranslationAttribute([NotNull] string text, [NotNull] string textPlural)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            TextPlural = textPlural ?? throw new ArgumentNullException(nameof(textPlural));
        }

        public string Context { get; set; }

        [NotNull]
        public string Text { get; }

        public string TextPlural { get; }
    }
}
