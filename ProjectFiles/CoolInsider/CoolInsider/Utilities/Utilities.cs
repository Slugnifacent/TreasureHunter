using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    public class Utilities
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * General Utilities used all over the ame
         * 
         */

        static Utilities util;
        Stopwatch clock;
        SpriteFont font;

        /// <summary>
        /// Constructor
        /// </summary>
        Utilities()
        {
            clock = new Stopwatch();
            font = TreasureHunter.content.Load<SpriteFont>(@"Fonts\Default");
        }

        /// <summary>
        /// Returns the singleton instance of this object
        /// </summary>
        /// <returns>Returns the static ScreenManager instance</returns>
        public static Utilities Instance()
        {
            if (util == null)
            {
                util = new Utilities();
            }
            return util;
        }

        /// <summary>
        /// Starts the utilities Stopwatch
        /// </summary>
        public void StartWatch()
        {
            clock.Start();
        }

        /// <summary>
        /// Restarts the utilities Stopwatch
        /// </summary>
        public void RestartWatch()
        {
            clock.Restart();
        }

        /// <summary>
        /// Elapsed time of the utilities stopwatch
        /// </summary>
        public TimeSpan ElapsedTime()
        {
            return clock.Elapsed;
        }

        /// <summary>
        /// Draws the elapesed time onto the screen
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        public void DrawTime(SpriteBatch batch)
        {
            TimeSpan span = clock.Elapsed;
            batch.DrawString(font, span.ToString(), new Vector2(50, 450), Color.White);

            batch.DrawString(font, Avatar.Instance().kinetics.position.ToString(), new Vector2(50, 500), Color.White);
        }

        /// <summary>
        /// Draws string to the Screen. Like a hud.
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        /// <param name="item">String to draw</param>
        /// <param name="Location">Location on the screen</param>
        public void DrawString(SpriteBatch batch, string item, Vector2 Location)
        {
            Location.X += TreasureHunter.Cam.Left;
            Location.Y += TreasureHunter.Cam.TOP;
            batch.DrawString(font, item, Location, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draws string to the Screen. Without Adjustment to Camera
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        /// <param name="item">String to draw</param>
        /// <param name="Location">Location on the screen</param>
        public void DrawStringNoCam(SpriteBatch batch, string item, Vector2 Location)
        {
            batch.DrawString(font, item, Location, Color.Black);
        }

        /// <summary>
        /// Wraps value around given max and min.
        /// </summary>
        /// <param name="Value">Value to test</param>
        /// <param name="Min">Minimum Value</param>
        /// <param name="Max">Maximum Value</param>
        public int Wrap(int Value, int Min, int Max) {
            if (Value < Min) return Max;
            if (Value > Max) return Min;
            return Value;
        }

        /// <summary>
        /// Wraps value around given max and min.
        /// </summary>
        /// <param name="Value">Value to test</param>
        /// <param name="Min">Minimum Value</param>
        /// <param name="Max">Maximum Value</param>
        public float Wrap(float Value, float Min, float Max)
        {
            if (Value < Min) return Max;
            if (Value > Max) return Min;
            return Value;
        }

        /// <summary>
        /// Test if Value is in bounds to given range
        /// </summary>
        /// <param name="Value">Value to test</param>
        /// <param name="Min">Minimum Value</param>
        /// <param name="Max">Maximum Value</param>
        public bool InBounds(int Value, int Min, int Max) {
            if (Value < Min) return false;
            if (Value > Max) return false;
            return true;
        }

    }
}
