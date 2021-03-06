// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Engine;
using Stride.Graphics;
using Stride.UI;
using Stride.UI.Controls;
using Stride.UI.Panels;

namespace AnimatedModel
{
    public class UIScript : StartupScript
    {
        public Entity Knight;

        public SpriteFont Font;

        public override void Start()
        {
            base.Start();

            // Bind the buttons
            var page = Entity.Get<UIComponent>().Page;

            var btnIdle = page.RootElement.FindVisualChildOfType<Button>("ButtonIdle");
            btnIdle.Click += (s, e) => Knight.Get<AnimationComponent>().Crossfade("Idle", TimeSpan.FromSeconds(0.25));

            var btnRun = page.RootElement.FindVisualChildOfType<Button>("ButtonRun");
            btnRun.Click += (s, e) => Knight.Get<AnimationComponent>().Crossfade("Run", TimeSpan.FromSeconds(0.25));

            // Set the default animation
            Knight.Get<AnimationComponent>().Play("Run");
        }        
    }
}
