using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    public abstract class Screen
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Common interface for all the screens in the game
         * 
         */

        /// <summary>
        /// Loads screen. Called when pushed onto the screen stack
        /// </summary>
        abstract public void Load();

        /// <summary>
        /// UnLoad screen. Called when popped off of the screen stack
        /// </summary>
        abstract public void UnLoad();

        /// <summary>
        /// Update the screen state
        /// </summary>
        abstract public void Update();

        /// <summary>
        /// Draws the current screen
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        abstract public void Draw(SpriteBatch batch);
    }
}
