// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Globalization;

using Xenko.Core.Annotations;

namespace Xenko.Core.Presentation.ValueConverters
{
    /// <summary>
    /// This converter will sum a given numeric value with a numeric value passed as parameter. You can use the <see cref="MarkupExtensions.DoubleExtension"/>
    /// markup extension to easily pass a double value as parameter, with the following syntax: {xk:Double (argument)}. 
    /// </summary>
    public class SumNum : ValueConverterBase<SumNum>
    {
        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var doubleValue = ConverterHelper.ConvertToDouble(value, culture);
            var doubleParameter = ConverterHelper.ConvertToDouble(parameter, culture);
            var result = doubleValue + doubleParameter;
            return System.Convert.ChangeType(result, value?.GetType() ?? targetType);
        }

        /// <inheritdoc/>
        public override object ConvertBack(object value, [NotNull] Type targetType, object parameter, CultureInfo culture)
        {
            var doubleValue = ConverterHelper.ConvertToDouble(value, culture);
            var doubleParameter = ConverterHelper.ConvertToDouble(parameter, culture);
            var result = doubleValue - doubleParameter;
            return System.Convert.ChangeType(result, targetType);
        }
    }
}
