// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xunit;

using Xenko.Core.Mathematics;
using Xenko.UI.Controls;

namespace Xenko.UI.Tests.Layering
{
    /// <summary>
    /// A class that contains test functions for layering of the <see cref="ScrollBar"/> class.
    /// </summary>
    [System.ComponentModel.Description("Tests for ScrollBar layering")]
    public class ScrollBarTests : ScrollBar
    {
        /// <summary>
        /// Test the invalidations generated object property changes.
        /// </summary>
        [Fact]
        public void TestBasicInvalidations()
        {
            // - test the properties that are not supposed to invalidate the object layout state
            UIElementLayeringTests.TestNoInvalidation(this, () => BarColor = new Color(1,2,3,4));
        }
    }
}
