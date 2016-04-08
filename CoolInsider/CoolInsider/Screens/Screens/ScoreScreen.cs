using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    class ScoreScreen : Screen
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Score Screen. Calculates the score from the previous level
         * 
         */

        Texture2D scorePage;
        DigitContainer<int> time;
        DigitContainer<int> combo;
        DigitContainer<int> discovered;
        DigitContainer<int> totalScore;
        bool enterContinue;

        Texture2D previousScreen;
        Timer Fade;
        Color color;
        /// <summary>
        /// Constructor. Fades in the Screen.
        /// </summary>
        /// <param name="Time">Time Level was beaten</param>
        /// <param name="Deaths">Deaths occoured withint the level</param>
        /// <param name="PreviousScreen">Previous screen texture</param>
        public ScoreScreen(int Time, int Combo, int Discovered, Texture2D PreviousScreen)
        {
            time = new DigitContainer<int>(0, 0, Time);
            discovered = new DigitContainer<int>(0, 0, Discovered);
            combo = new DigitContainer<int>(0, 0, Combo);
            totalScore = new DigitContainer<int>(0, 0, 0);
            totalScore.value = 0;
            totalScore.min = 0;
            totalScore.max += (Time);
            totalScore.max += Discovered;
            totalScore.max += Combo;

            time.increment = (int)((time.max - time.value) * .05f);
            combo.increment = (int)((combo.max - combo.value) * .05f);
            discovered.increment = (int)((discovered.max - discovered.value) * .05f);
            totalScore.increment = (int)((totalScore.max - totalScore.value) * .05f);


            if (time.increment == 0) time.increment = 1;
            if (combo.increment == 0) combo.increment = 1;
            if (discovered.increment == 0) discovered.increment = 1;
            if (totalScore.increment == 0) totalScore.increment = 1;


            previousScreen = PreviousScreen;
            color = Color.White;
            Fade = new Timer(.005f);
            Fade.Start();
        }

        /// <summary>
        /// Loads screen. Called when pushed onto the screen stack
        /// </summary>
        public override void Load()
        {
            scorePage = TreasureHunter.content.Load<Texture2D>(@"ScreenShots\ScreenShot");
            SoundManager.Instance().AddSound(new SFX("SoundEffects\\Applause"));
        }

        /// <summary>
        /// Value grows to the given goal
        /// </summary>
        /// <param name="Value">Value to grow</param>
        /// <param name="Goal">Target value</param>
        /// <param name="Amount">Growth amount</param>
        /// <returns>Returns true if goal is met or surpassed, False otherwise</returns>
        bool GrowToGoal(ref int Value, int Goal, int Amount)
        {
            if (Value < Goal)
            {
                Value += Amount;
                return false;
            }
            Value = Goal;
            return true;
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
            if (color.A == 0)
            {
                if (GrowToGoal(ref time.value, time.max, time.increment))
                {
                    if (GrowToGoal(ref discovered.value, discovered.max, discovered.increment))
                    {
                        if (GrowToGoal(ref combo.value, combo.max, combo.increment))
                        {
                            if (GrowToGoal(ref totalScore.value, totalScore.max, totalScore.increment))
                            {
                                enterContinue = true;
                                if (ControllerInput.Instance().GetKey(Keys.Enter).Pressed)
                                {
                                    ScreenManager.Instance().Pop();
                                }
                            }
                        }
                    }
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
            TreasureHunter.graphics.GraphicsDevice.Clear(Color.Transparent);
            batch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            batch.Draw(scorePage, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            batch.Draw(previousScreen, Vector2.Zero, null, color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            Utilities.Instance().DrawStringNoCam(batch, "Time Left  :          " + time.value, new Vector2(100, 150));
            Utilities.Instance().DrawStringNoCam(batch, "Discovered :          " + discovered.value, new Vector2(100, 180));
            Utilities.Instance().DrawStringNoCam(batch, "Max Combo  :          " + combo.value, new Vector2(100, 210));
            Utilities.Instance().DrawStringNoCam(batch, "Total Score:   " + totalScore.value, new Vector2(100, 240));
            if (enterContinue)
            {
                Utilities.Instance().DrawStringNoCam(batch, "Press Enter to Continue", new Vector2(100, 320));
            }
            batch.End();
        }
    }
}
