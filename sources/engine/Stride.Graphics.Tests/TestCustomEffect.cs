// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Threading.Tasks;

using Stride.Core.Mathematics;
using Stride.Rendering;
using Stride.Games;

using Xunit;

namespace Stride.Graphics.Tests
{
    public static class MyCustomShaderKeys
    {
        public static readonly ValueParameterKey<Vector4> ColorFactor2 = ParameterKeys.NewValue<Vector4>();
    }

    public class TestCustomEffect : GraphicTestGameBase
    {
        private DynamicEffectInstance effectInstance;

        private float switchEffectLevel;

        public TestCustomEffect() { }

        protected override void RegisterTests()
        {
            base.RegisterTests();

            FrameGameSystem.Draw(DrawCustomEffect).TakeScreenshot();
        }

        protected override async Task LoadContent()
        {
            await base.LoadContent();

            effectInstance = new DynamicEffectInstance("CustomEffect.CustomSubEffect");
            effectInstance.Initialize(Services);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (!ScreenShotAutomationEnabled)
                DrawCustomEffect();
        }

        private void DrawCustomEffect()
        {
            GraphicsContext.CommandList.Clear(GraphicsDevice.Presenter.BackBuffer, Color.Black);
            GraphicsContext.CommandList.Clear(GraphicsDevice.Presenter.DepthStencilBuffer, DepthStencilClearOptions.DepthBuffer);
            GraphicsContext.CommandList.SetRenderTargetAndViewport(GraphicsDevice.Presenter.DepthStencilBuffer, GraphicsDevice.Presenter.BackBuffer);

            effectInstance.Parameters.Set(MyCustomShaderKeys.ColorFactor2, (Vector4) Color.Red);
            effectInstance.Parameters.Set(CustomShaderKeys.SwitchEffectLevel, switchEffectLevel);
            effectInstance.Parameters.Set(TexturingKeys.Texture0, UVTexture);
            switchEffectLevel++; // TODO: Add switch Effect to test and capture frames

            GraphicsContext.DrawQuad(effectInstance);
        }

        /// <summary>
        /// Run the test
        /// </summary>
        [Fact]
        public void RunCustomEffect()
        {
            RunGameTest(new TestCustomEffect());
        }
    }
}
