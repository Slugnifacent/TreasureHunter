using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace TreasureHunter
{
    class GameScreen : Screen
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Gameplay Screen
         * 
         */

        /// <summary>
        /// Constructor
        /// </summary>
        public GameScreen() { }

        /// <summary>
        /// Loads screen. Called when pushed onto the screen stack
        /// </summary>
        public override void Load()
        {
            LevelManager.Instance();
            
            LevelManager.Instance().GenerateFirstLevel();
            TreasureHunter.Cam = new Camera(new Vector2(0, 0), new Rectangle(0, 0,
                LevelManager.Instance().getCurrentLevel().ScaledWidth(),
                LevelManager.Instance().getCurrentLevel().ScaledHeight()));

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
            TreasureHunter.Cam.SetTarget(Avatar.Instance().kinetics.position);
            TreasureHunter.Cam.Update();

            LevelManager.Instance().Update();
        }

        /// <summary>
        /// Draws the current screen
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        public override void Draw(SpriteBatch batch)
        {
            TreasureHunter.graphics.GraphicsDevice.SetRenderTarget(LevelManager.Instance().renderTarget);
            TreasureHunter.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            LevelManager.Instance().Draw(batch);
        }

    }
}
