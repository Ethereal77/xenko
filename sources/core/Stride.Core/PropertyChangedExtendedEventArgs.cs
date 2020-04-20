// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.ComponentModel;
using System.Reflection;

using Stride.Core.Annotations;

namespace Stride.Core
{
    public class PropertyChangedExtendedEventArgs : PropertyChangedEventArgs
    {
        public PropertyChangedExtendedEventArgs([NotNull] PropertyInfo propertyInfo, object oldValue, object newValue) : base(propertyInfo.Name)
        {
            PropertyInfo = propertyInfo;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public PropertyInfo PropertyInfo { get; private set; }
        public object NewValue { get; private set; }
        public object OldValue { get; private set; }
    }
}
