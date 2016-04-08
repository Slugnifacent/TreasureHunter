using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace TreasureHunter
{
    class ControllerInput
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Controller Class that registers Keyboard input. 
         * 
         */

        static ControllerInput input;
        Dictionary<Keys, Button> KeyboardButtons;
        Dictionary<Buttons, Button> ButtonsButtons;

        /// <summary>
        /// Constructor. Add Keys here for the game to register them
        /// </summary>
        ControllerInput() {
            KeyboardButtons = new Dictionary<Keys, Button>();
            AddKey(Keys.Up);
            AddKey(Keys.Down);
            AddKey(Keys.Left);
            AddKey(Keys.Right);
            AddKey(Keys.Escape);
            AddKey(Keys.Enter);
            AddKey(Keys.Space);

            ButtonsButtons = new Dictionary<Buttons, Button>();
            AddButton(Buttons.A);
            AddButton(Buttons.B);
            AddButton(Buttons.X);
            AddButton(Buttons.Y);
            AddButton(Buttons.LeftShoulder);
            AddButton(Buttons.RightShoulder);
            AddButton(Buttons.LeftTrigger);
            AddButton(Buttons.RightTrigger);
        }

        /// <summary>
        /// Returns the singleton instance of this object
        /// </summary>
        /// <returns>Returns the static ControllerInput instance</returns>
        public static ControllerInput Instance() {
            if (input == null) {
                input = new ControllerInput();
            }
            return input;
        }

        /// <summary>
        /// Updates the Controllers
        /// </summary>
        public void Update() {
            foreach (KeyValuePair<Buttons, Button> button in ButtonsButtons)
            {
                button.Value.Update();
            }
            foreach (KeyValuePair<Keys, Button> button in KeyboardButtons)
            {
                button.Value.Update();
            }
        }

        /// <summary>
        /// Gets the Game Button used for State Checking
        /// </summary>
        /// <param name="_Key">Desired key to check</param>
        /// <returns>Returns Game button</returns>
        void AddButton(Buttons Button)
        {
            if (!ButtonsButtons.ContainsKey(Button))
            {
                ButtonsButtons[Button] = new Button(Button);
            }
        }

        /// <summary>
        /// Adds Key buttons to the list of recognizable buttons
        /// </summary>
        /// <param name="key"></param>
        void AddKey(Keys Key)
        {
            if(!KeyboardButtons.ContainsKey(Key)){
            KeyboardButtons[Key] = new Button(Key);
            }
        }

        /// <summary>
        /// Returns selected button
        /// </summary>
        /// <param name="Button">Button to return</param>
        /// <returns>Desired button</returns>
        public Button GetButton(Buttons Button) {
            return ButtonsButtons[Button];
        }

        /// <summary>
        /// Returns selected keyboard button
        /// </summary>
        /// <param name="Button">Keyboard button to return</param>
        /// <returns>Keyboard button desired button</returns>
        public Button GetKey(Keys Button)
        {
            return KeyboardButtons[Button];
        }

        /// <summary>
        /// Returns PlayerIndex.One GamePadThumbSticks
        /// </summary>
        public GamePadThumbSticks Sticks
        {
            get { return GamePad.GetState(PlayerIndex.One).ThumbSticks; }
        }

        /// <summary>
        /// Returns PlayerIndex.One GamePadTriggers
        /// </summary>
        public GamePadTriggers Triggers
        {
            get { return GamePad.GetState(PlayerIndex.One).Triggers; }
        }
    }
}
