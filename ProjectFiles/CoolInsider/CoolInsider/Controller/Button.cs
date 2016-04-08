using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TreasureHunter
{
    class Button
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Class used to register the status of game buttons
         * 
         */

        delegate void UPDATE();
        UPDATE update;

        Buttons button;
        Keys key;
        

        bool justPressed;
        bool justReleased;
        bool held;

        /// <summary>
        /// Constructor. Assings this button as a key value
        /// </summary>
        /// <param name="Key">Key value to assign</param>
        public Button(Keys Key)
        {
            update = KeyUpdate;
            key = Key;
        }

        /// <summary>
        /// Constructor. Assings this button as a Buttons value
        /// </summary>
        /// <param name="Key">Buttons value to assign</param>
        public Button(Buttons Butt)
        {
            update = ButtonUpdate;
            button = Butt;
        }

        /// <summary>
        /// Updates the game button
        /// </summary>
        public void Update()
        {
            justPressed = false;
            justReleased = false;
            update();
        }

        /// <summary>
        /// Updates if button is a key
        /// </summary>
        void KeyUpdate()
        {
            if (Keyboard.GetState().IsKeyDown(key) && !held)
            {
                if (justPressed == false) {
                    justPressed = true;
                }
                held = true;
            }

            if (Keyboard.GetState().IsKeyUp(key))
            {
                if (held)
                {
                    justReleased = true;
                }
                held = false;
            }
           
        }

        /// <summary>
        /// Updates if button is a button
        /// </summary>
        void ButtonUpdate()
        {
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(button) && !held)
            {
                if (justPressed == false)
                {
                    justPressed = true;
                }
                held = true;
            }

            if (GamePad.GetState(PlayerIndex.One).IsButtonUp(button))
            {
                if (held)
                {
                    justReleased = true;
                }
                held = false;
            }
        }

        /// <summary>
        /// Returns true if button is being held. False otherwise
        /// </summary>
        public bool Held
        { 
            get{return held;}
        }

        /// <summary>
        /// Returns true if button has just been pressed. False otherwise
        /// </summary>
        public bool Pressed
        {
            get { return justPressed; }
        }

        /// <summary>
        /// Returns true if button has just been released. False otherwise
        /// </summary>
        public bool Released
        {
            get { return justReleased; }
        }
    }
}
