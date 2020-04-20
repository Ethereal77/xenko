// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Globalization;

using Stride.Core.IO;

namespace Stride.Core.Presentation.ValueConverters
{
    /// <summary>
    /// This converter will convert an <see cref="UFile"/> to an instance of the <see cref="Uri"/> class.
    /// </summary>
    public class UFileToUri : OneWayValueConverter<UFileToUri>
    {
        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            try
            {
                var uri = new Uri((UFile)value);
                return uri;
            }
            catch
            {
                return null;
            }
        }
    }
}
