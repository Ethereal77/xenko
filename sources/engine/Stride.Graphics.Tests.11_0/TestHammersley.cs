// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Threading.Tasks;

using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Rendering;
using Stride.Rendering.ComputeEffect;
using Stride.Games;
using Stride.Input;

using Xunit;

namespace Stride.Graphics.Tests
{
    /// <summary>
    ///   Test class for Hammersley sampling shader.
    /// </summary>
    public class TestHammersley : GraphicTestGameBase
    {
        private Texture output;

        private const int OutputSize = 512;

        private int samplesCount = 1024;
        private ComputeEffectShader renderHammersley;

        public TestHammersley()
        {
            GraphicsDeviceManager.PreferredBackBufferWidth = OutputSize;
            GraphicsDeviceManager.PreferredBackBufferHeight = OutputSize;
        }

        protected override async Task LoadContent()
        {
            await base.LoadContent();

            var context = RenderContext.GetShared(Services);
            renderHammersley = new ComputeEffectShader(context) { ShaderSourceName = "HammersleyTest" };

            output = Texture.New2D(GraphicsDevice, OutputSize, OutputSize, PixelFormat.R8G8B8A8_UNorm, TextureFlags.ShaderResource | TextureFlags.UnorderedAccess | TextureFlags.RenderTarget).DisposeBy(this);
        }

        protected override void RegisterTests()
        {
            base.RegisterTests();

            FrameGameSystem.TakeScreenshot();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Input.IsKeyPressed(Keys.NumPad1))
                samplesCount = Math.Max(1, samplesCount / 2);

            if (Input.IsKeyPressed(Keys.NumPad3))
                samplesCount = Math.Min(1024, samplesCount * 2);
        }

        protected override void Draw(GameTime gameTime)
        {
            var renderDrawContext = new RenderDrawContext(Services, RenderContext.GetShared(Services), GraphicsContext);

            GraphicsContext.CommandList.Clear(output, Color4.White);
            renderHammersley.ThreadGroupCounts = new Int3(samplesCount, 1, 1);
            renderHammersley.ThreadNumbers = new Int3(1);
            renderHammersley.Parameters.Set(HammersleyTestKeys.OutputTexture, output);
            renderHammersley.Parameters.Set(HammersleyTestKeys.SamplesCount, samplesCount);
            renderHammersley.Draw(renderDrawContext);

            GraphicsContext.DrawTexture(output);

            base.Draw(gameTime);
        }

        /// <summary>
        ///   Run the test.
        /// </summary>
        [SkippableFact]
        public void RunImageLoad()
        {
            RunGameTest(new TestHammersley());
        }
    }
}
