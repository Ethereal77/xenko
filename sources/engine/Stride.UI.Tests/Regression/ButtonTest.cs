// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Threading.Tasks;

using Xunit;

using Stride.UI.Controls;

namespace Stride.UI.Tests.Regression
{
    /// <summary>
    /// Class for rendering tests on the <see cref="Button"/> 
    /// </summary>
    public class ButtonTest : UITestGameBase
    {
        private Button button;

        public ButtonTest()
        {
        }

        protected override async Task LoadContent()
        {
            await base.LoadContent();

            button = new Button();
            ApplyButtonDefaultStyle(button);

            UIComponent.Page = new Engine.UIPage { RootElement = button };
        }

        protected override void RegisterTests()
        {
            base.RegisterTests();

            FrameGameSystem.DrawOrder = -1;
            FrameGameSystem.TakeScreenshot();
            FrameGameSystem.Draw(DrawTest1).TakeScreenshot();
        }

        private void DrawTest1()
        {
            button.RaiseTouchDownEvent(new TouchEventArgs());
        }

        [Fact]
        public void RunButtonTest()
        {
            RunGameTest(new ButtonTest());
        }

        /// <summary>
        /// Launch the Image test.
        /// </summary>
        internal static void Main()
        {
            using (var game = new ButtonTest())
                game.Run();
        }
    }
}
