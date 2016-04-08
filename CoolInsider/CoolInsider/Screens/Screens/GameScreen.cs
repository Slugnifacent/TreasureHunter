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
            
            CameraManager.Instance().AddCamera(new Camera(new Vector2(0, 0), new Rectangle(0, 0,
                LevelManager.Instance().getCurrentLevel().LevelWidth(),
                LevelManager.Instance().getCurrentLevel().LevelHeight())));

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
            CameraManager.Instance().GetCamera().SetTarget(Avatar.Instance().kinetics.position);
            CameraManager.Instance().GetCamera().Update();

            LevelManager.Instance().Update();
        }

        /// <summary>
        /// Draws the current screen
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        public override void Draw(SpriteBatch batch)
        {
            TreasureHunter.graphics.GraphicsDevice.Clear(Color.Transparent);
            LevelManager.Instance().Draw(batch);
        }

    }
}
