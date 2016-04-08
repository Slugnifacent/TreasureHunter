using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TreasureHunter
{
    class ScreenManager
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Common interface for all the screens in the game
         * 
         */

        static ScreenManager manager;
        Title titleScreen;
        Stack<Screen> ScreenStack;
        bool exitGame;

        /// <summary>
        /// Constructor
        /// </summary>
        ScreenManager()
        {
            ScreenStack = new Stack<Screen>();
            titleScreen = new Title();
            Push(titleScreen);
            ControllerInput.Instance();
            SoundManager.Instance();
            Utilities.Instance();
            SoundManager.Instance().BGM(new SongTrack(@"Soundtrack\\Xero_Chi"));
            SoundManager.Instance().BGM().Play();
        }

        /// <summary>
        /// Returns the singleton instance of this object
        /// </summary>
        /// <returns>Returns the static ScreenManager instance</returns>
        public static ScreenManager Instance()
        {
            if (manager == null)
            {
                manager = new ScreenManager();
            }
            return manager;
        }

        /// <summary>
        /// Updates the current Screen
        /// </summary>
        public void Update()
        {
            if (ScreenStack.Count <= 0) {
                exitGame = true;
                return;
            }
            ControllerInput.Instance().Update();
            SoundManager.Instance().Update();
            ScreenStack.Peek().Update();
            if (!SoundManager.Instance().BGM().Playing()) {
                SoundManager.Instance().BGM().Play();
            }
            if (ControllerInput.Instance().GetKey(Keys.Escape).Pressed)
            {
                ScreenManager.Instance().Pop();
            }
        }

        /// <summary>
        /// Draws the current screen
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        public void Draw(SpriteBatch batch)
        {
            if (ScreenStack.Count <= 0)
            {
                exitGame = true;
                return;
            }
            ScreenStack.Peek().Draw(batch);
        }

        /// <summary>
        /// Pops a screen off of the stack
        /// </summary>
        public void Pop()
        {
            ScreenStack.Peek().UnLoad();
            ScreenStack.Pop();
            if (ScreenStack.Count <= 0)
            {
                exitGame = true;
                return;
            }
        }

        /// <summary>
        /// Pushes a new screen onto the stack
        /// </summary>
        /// <param name="NewScreen">Screen to push on the stack</param>
        public void Push(Screen NewScreen)
        {
            ScreenStack.Push(NewScreen);
            NewScreen.Load();
        }

        /// <summary>
        /// Exit game flag.
        /// </summary>
        /// <returns>True if exitGame flag is true, false otherwise</returns>
        public bool ExitGame() {
            return exitGame; 
        }
    }
}
