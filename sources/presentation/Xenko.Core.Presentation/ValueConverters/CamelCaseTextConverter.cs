// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Globalization;

using Xenko.Core.Presentation.Core;

namespace Xenko.Core.Presentation.ValueConverters
{
    /// <summary>
    /// This converter will format a CamelCase string by inserting spaces between words.
    /// </summary>
    public class CamelCaseTextConverter : OneWayValueConverter<CamelCaseTextConverter>
    {
        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var strVal = value.ToString();
            return Utils.SplitCamelCase(strVal);
        }
    }
}
