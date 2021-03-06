// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Threading.Tasks;

using Stride.Graphics;
using Stride.Rendering.Sprites;
using Stride.UI.Controls;
using Stride.UI.Panels;

using Xunit;

namespace Stride.UI.Tests.Regression
{
    /// <summary>
    ///   Test class for rendering tests on the <see cref="UniformGrid"/>
    /// </summary>
    public class UniformGridTest : UITestGameBase
    {
        protected override void RegisterTests()
        {
            base.RegisterTests();

            FrameGameSystem.TakeScreenshot();
        }

        protected override async Task LoadContent()
        {
            await base.LoadContent();

            var imgElt = new ImageElement { Source = (SpriteFromTexture)new Sprite(Content.Load<Texture>("uv")), StretchType = StretchType.Fill };
            imgElt.DependencyProperties.Set(GridBase.RowSpanPropertyKey, 2);
            imgElt.DependencyProperties.Set(GridBase.ColumnSpanPropertyKey, 2);
            imgElt.DependencyProperties.Set(GridBase.RowPropertyKey, 1);
            imgElt.DependencyProperties.Set(GridBase.ColumnPropertyKey, 1);

            var button1 = new Button();
            ApplyButtonDefaultStyle(button1);
            button1.DependencyProperties.Set(GridBase.RowPropertyKey, 3);
            button1.DependencyProperties.Set(GridBase.ColumnPropertyKey, 0);

            var button2 = new Button();
            ApplyButtonDefaultStyle(button2);
            button2.DependencyProperties.Set(GridBase.RowPropertyKey, 3);
            button2.DependencyProperties.Set(GridBase.ColumnPropertyKey, 3);

            var text = new TextBlock
            {
                Text = "Test Uniform Grid",
                Font = Content.Load<SpriteFont>("MicrosoftSansSerif15"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            ApplyTextBlockDefaultStyle(text);
            text.DependencyProperties.Set(GridBase.ColumnSpanPropertyKey, 2);
            text.DependencyProperties.Set(GridBase.RowPropertyKey, 0);
            text.DependencyProperties.Set(GridBase.ColumnPropertyKey, 1);

            var grid = new UniformGrid { Rows = 4, Columns = 4};
            grid.Children.Add(imgElt);
            grid.Children.Add(button1);
            grid.Children.Add(button2);
            grid.Children.Add(text);

            UIComponent.Page = new Engine.UIPage { RootElement = grid };
        }

        [Fact]
        public void RunUniformGridTest()
        {
            RunGameTest(new UniformGridTest());
        }
    }
}
