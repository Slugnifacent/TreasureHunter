using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace TreasureHunter
{
    class Title : Screen
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Title Screen
         * 
         */

        Texture2D titlePage;

        /// <summary>
        /// Constructor
        /// </summary>
        public Title() { }

        /// <summary>
        /// Loads screen. Called when pushed onto the screen stack
        /// </summary>
        public override void Load()
        {
            titlePage = TreasureHunter.content.Load<Texture2D>(@"ScreenShots\TitleScreen");
        }

        /// <summary>
        /// UnLoad screen. Called when popped off of the screen stack
        /// </summary>
        public override void UnLoad()
        {
        }

        /// <summary>
        /// Update the screen state
        /// </summary>
        public override void Update()
        {
            if (ControllerInput.Instance().GetKey(Keys.Enter).Pressed) {
                ScreenManager.Instance().Push(new GameScreen());
            }
        }

        /// <summary>
        /// Draws the current screen
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        public override void Draw(SpriteBatch batch)
        {
            batch.Begin();
            batch.Draw(titlePage, Vector2.Zero, Color.White);
            batch.End();
        }
    }
}
