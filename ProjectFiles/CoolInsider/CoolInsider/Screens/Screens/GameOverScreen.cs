using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace TreasureHunter
{
    class GameOverScreen : Screen
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Gameover Screen
         * 
         */

        Texture2D gameOverPage;
        Texture2D previousScreen;
        Timer Fade;
        Color color;

        /// <summary>
        /// Constructor. Fades in the Screen.
        /// </summary>
        /// <param name="PreviousScreen">Previous screen texture</param>
        public GameOverScreen(Texture2D PreviousScreen) { 
            gameOverPage = TreasureHunter.content.Load<Texture2D>(@"ScreenShots\Game Over Screen");
            previousScreen = PreviousScreen;
            color = Color.White;
            Fade = new Timer(.001f);
            Fade.Start();
        }

        /// <summary>
        /// Loads screen. Called when pushed onto the screen stack
        /// </summary>
        public override void Load()
        {
            SoundManager.Instance().BGM().Stop();
            SoundManager.Instance().BGM(new SongTrack(@"Soundtrack\\smwfull"));
            SoundManager.Instance().BGM().Play();
        }

        /// <summary>
        /// UnLoad screen. Called when popped off of the screen stack
        /// </summary>
        public override void UnLoad()
        {
            SoundManager.Instance().BGM().Stop();
            SoundManager.Instance().BGM(new SongTrack(@"Soundtrack\\Xero_Chi"));
            SoundManager.Instance().BGM().Play();
        }

        /// <summary>
        /// Update the screen state
        /// </summary>
        public override void Update()
        {
            if(color.A == 0){
                if (ControllerInput.Instance().GetKey(Keys.Enter).Pressed) {
                    ScreenManager.Instance().Pop();
                }
            }
            if (Fade.Ready())
            {
                int temp = color.A;
                if ((temp - 5) < 0)
                {
                    color.A = 0;
                }
                else color.A -= 5;
                Fade.Reset();
                Fade.Start();
            }
            Fade.Update();
        }

        /// <summary>
        /// Draws the current screen
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        public override void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.BackToFront,BlendState.NonPremultiplied);
            batch.Draw(gameOverPage, Vector2.Zero,null, Color.White,0,Vector2.Zero,1,SpriteEffects.None,1);
            batch.Draw(previousScreen, Vector2.Zero, null, color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            batch.End();
        }
    }
}


