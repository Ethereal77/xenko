// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.Generic;

using Stride.Core.Presentation.Dirtiables;

namespace Stride.Core.Presentation.Tests.Dirtiables
{
    public class SimpleDirtiable : IDirtiable
    {
        public bool IsDirty { get; private set; }

        public IEnumerable<IDirtiable> Yield()
        {
            yield return this;
        }

        public void UpdateDirtiness(bool value)
        {
            IsDirty = value;
        }
    }
}
