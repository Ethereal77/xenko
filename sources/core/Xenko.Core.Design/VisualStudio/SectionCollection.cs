// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2009 SLNTools - Christian Warren
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

using Xenko.Core.Annotations;

namespace Xenko.Core.VisualStudio
{
    /// <summary>
    /// A collection of <see cref="Section"/>
    /// </summary>
    [DebuggerDisplay("Count = {" + nameof(Count) + "}")]
    public sealed class SectionCollection
        : KeyedCollection<string, Section>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionCollection"/> class.
        /// </summary>
        public SectionCollection()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionCollection"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public SectionCollection(IEnumerable<Section> items)
            : this()
        {
            this.AddRange(items);
        }

        protected override string GetKeyForItem([NotNull] Section item)
        {
            return item.Name;
        }

        protected override void InsertItem(int index, [NotNull] Section item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            // Add a clone of the item instead of the item itself
            base.InsertItem(index, item.Clone());
        }

        protected override void SetItem(int index, [NotNull] Section item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            // Add a clone of the item instead of the item itself
            base.SetItem(index, item.Clone());
        }
    }
}
