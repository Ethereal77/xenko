// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Games.Testing;

using Xunit;

namespace Stride.Samples.Tests
{
    public class FPSTest : IClassFixture<FPSTest.Fixture>
    {
        private const string Path = "..\\..\\..\\..\\..\\samplesGenerated\\FirstPersonShooter";

        public class Fixture : SampleTestFixture
        {
            public Fixture() : base(Path, new Guid("B12AF970-1F11-4BC8-9571-3B4DA9E20F05"))
            {
            }
        }

        [Fact]
        public void TestLaunch()
        {
            using (var game = new GameTestingClient(Path, SampleTestsData.TestPlatform))
            {
                game.Wait(TimeSpan.FromMilliseconds(2000));
            }
        }

        [Fact]
        public void TestInputs()
        {
            using (var game = new GameTestingClient(Path, SampleTestsData.TestPlatform))
            {
                game.Wait(TimeSpan.FromMilliseconds(2000));
                game.Tap(new Vector2(0.5f, 0.7f), TimeSpan.FromMilliseconds(500));
                game.Wait(TimeSpan.FromMilliseconds(500));
                game.KeyPress(Keys.Space, TimeSpan.FromMilliseconds(500));
                game.Wait(TimeSpan.FromMilliseconds(500));
                game.TakeScreenshot();
                game.Wait(TimeSpan.FromMilliseconds(500));
            }
        }
    }
}
