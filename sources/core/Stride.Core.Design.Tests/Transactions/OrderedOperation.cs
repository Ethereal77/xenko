// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Xunit;

namespace Stride.Core.Design.Tests.Transactions
{
    internal class OrderedOperation : SimpleOperation
    {
        private readonly Counter counter;
        private readonly int order;
        private readonly int totalCount;

        internal class Counter
        {
            public void Reset() => Value = 0;
            public int Value { get; set; }
        }

        public OrderedOperation(Counter counter, int order, int totalCount)
        {
            this.counter = counter;
            this.order = order;
            this.totalCount = totalCount;
        }

        protected override void Rollback()
        {
            // Rollback is done in reverse order
            var value = totalCount - order - 1;
            Assert.Equal(value, counter.Value);
            counter.Value++;
            base.Rollback();
        }

        protected override void Rollforward()
        {
            Assert.Equal(order, counter.Value);
            counter.Value++;
            base.Rollforward();
        }
    }
}
