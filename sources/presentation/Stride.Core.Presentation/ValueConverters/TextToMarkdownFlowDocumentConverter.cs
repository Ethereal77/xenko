// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Documents;

namespace Stride.Core.Presentation.ValueConverters
{
    [ValueConversion(typeof(string), typeof(FlowDocument))]
    public class TextToMarkdownFlowDocumentConverter : OneWayValueConverter<TextToMarkdownFlowDocumentConverter>
    {
        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && !(parameter is XamlMarkdown))
            {
                throw new ArgumentException($"The parameter of this converter must be an instance of the {nameof(XamlMarkdown)} class.");
            }

            if (value == null)
                return null;

            var engine = (XamlMarkdown)parameter ?? defaultMarkdown.Value;
            if (engine == null)
                return null;

            try
            {
                var text = value.ToString();
                return engine.Transform(text);
            }
            catch (ArgumentException) { }
            catch (FormatException) { }
            catch (InvalidOperationException) { }

            return null;
        }

        private readonly Lazy<XamlMarkdown> defaultMarkdown = new Lazy<XamlMarkdown>(() => new XamlMarkdown());
    }
}
