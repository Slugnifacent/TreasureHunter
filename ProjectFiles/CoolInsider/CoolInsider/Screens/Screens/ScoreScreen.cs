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
        int time;
        int deaths;
        int totalScore;

        int timeEffect;
        int deathsEffect;
        int totalScoreEffect;

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
        public ScoreScreen(int Time, int Deaths,Texture2D PreviousScreen) {
            time = Time;
            deaths = Deaths;
            totalScore  += (Time) * 100;
            totalScore  /= deaths+1;

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
        bool GrowToGoal(ref int Value, int Goal, int Amount) {
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
            if (GrowToGoal(ref timeEffect, time, 1) && color.A == 0)
            {
                if (GrowToGoal(ref deathsEffect, deaths, 1))
                {
                    if (GrowToGoal(ref totalScoreEffect, totalScore, 4))
                    {
                        enterContinue = true;
                        if (ControllerInput.Instance().GetKey(Keys.Enter).Pressed)
                        {
                            ScreenManager.Instance().Pop();
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
            batch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            batch.Draw(scorePage, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            batch.Draw(previousScreen, Vector2.Zero, null, color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            Utilities.Instance().DrawStringNoCam(batch, "Time Left:          " + timeEffect, new Vector2(100, 150));
            Utilities.Instance().DrawStringNoCam(batch, "Total Score:   " + totalScoreEffect, new Vector2(100, 220));
            if (enterContinue)
            {
                Utilities.Instance().DrawStringNoCam(batch, "Press Enter to Continue", new Vector2(100, 320));
            }
            
            batch.End();
        }
    }
}
