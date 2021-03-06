// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Stride.Core.Extensions;

namespace Stride.Core.Presentation.ValueConverters
{
    public class Take : OneWayValueConverter<Take>
    {
        /// <inheritdoc />
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return value;

            var count = ConverterHelper.TryConvertToInt32(parameter, culture);
            return count.HasValue ? value.ToEnumerable<object>().Take(count.Value) : value;
        }
    }
}
