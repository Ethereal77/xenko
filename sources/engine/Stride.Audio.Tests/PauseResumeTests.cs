// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Threading.Tasks;

using Stride.Games;
using Stride.Input;

using Xunit;

namespace Stride.Audio.Tests
{
    public class PauseResumeTest : AudioTestGame
    {
        private SoundInstance music;
        private SoundInstance effect;

        public PauseResumeTest() { }

        protected override async Task LoadContent()
        {
            await base.LoadContent();

            music = Content.Load<Sound>("MusicFishLampMp3").CreateInstance(Audio.AudioEngine.DefaultListener);
            effect = Content.Load<Sound>("EffectBip").CreateInstance(Audio.AudioEngine.DefaultListener);
            music.IsLooping = true;
            effect.IsLooping = true;
            music.Play();
            effect.Play();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var pointerEvent in Input.PointerEvents)
            {
                if (pointerEvent.EventType == PointerEventType.Released)
                {
                    music.Stop();
                    music.Play();
                    //effect.Stop();
                    //effect.Play();
                }
            }
        }

        [Fact]
        public void RunPauseGame()
        {
            RunGameTest(new PauseResumeTest());
        }
    }
}
