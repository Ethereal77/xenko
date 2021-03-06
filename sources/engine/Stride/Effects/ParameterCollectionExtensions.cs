// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Stride.Rendering
{
    /// <summary>
    /// Extensions for <see cref="ParameterCollection"/>.
    /// </summary>
    public static class ParameterCollectionExtensions
    {
        public static string ToStringPermutationsDetailed(this ParameterCollection parameterCollection)
        {
            var builder = new StringBuilder();

            var first = true;
            foreach (var usedParam in parameterCollection.ParameterKeyInfos)
            {
                // Ignore any non-permutation key
                if (usedParam.Key.Type != ParameterKeyType.Permutation)
                    continue;

                var value = parameterCollection.ObjectValues[usedParam.BindingSlot];

                builder.Append("@P ");
                if (first)
                {
                    builder.Append("  - ");
                    first = false;
                }

                if (usedParam.Key == null)
                    builder.Append("null");
                else
                    builder.Append(usedParam.Key);
                builder.Append(": ");
                if (value == null)
                {
                    builder.AppendLine("null");
                }
                else
                {
                    if (value is Array || value is IList)
                    {
                        builder.AppendLine(string.Join(", ", (IEnumerable<object>)value));
                    }
                    else
                    {
                        builder.AppendLine(value.ToString());
                    }
                }
            }

            return builder.ToString();
        }
    }
}
