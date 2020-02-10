// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

using Xenko.Core;
using Xenko.Core.Mathematics;
using Xenko.Input;
using Xenko.Games.Testing;

using Xunit;

namespace Xenko.Samples.Tests
{
    public class SpriteStudioDemoTest : IClassFixture<SpriteStudioDemoTest.Fixture>
    {
        private const string Path = "..\\..\\..\\..\\..\\samplesGenerated\\SpriteStudioDemo";

        public class Fixture : SampleTestFixture
        {
            public Fixture() : base(Path, new Guid("6BE30E8D-9346-4130-87BE-12BF9CC362DE"))
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

                game.Tap(new Vector2(0.83f, 0.05f), TimeSpan.FromMilliseconds(500));
                game.Wait(TimeSpan.FromMilliseconds(2000));
                game.TakeScreenshot();
                game.Wait(TimeSpan.FromMilliseconds(500));

                game.KeyPress(Keys.Space, TimeSpan.FromMilliseconds(200));
                game.Wait(TimeSpan.FromMilliseconds(100));
                game.TakeScreenshot();
                game.Wait(TimeSpan.FromMilliseconds(500));
            }
        }
    }
}
