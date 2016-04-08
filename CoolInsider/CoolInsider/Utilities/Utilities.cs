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
         * General Utilities used all over the game
         * 
         */

        static Utilities util;
        SpriteFont font;
        public Random rand;
        DateTime timestamp;
        DateTime prevTimeStamp;


        /// <summary>
        /// Constructor
        /// </summary>
        Utilities()
        {
            font = TreasureHunter.content.Load<SpriteFont>(@"Fonts\Default");
            rand = new Random(DateTime.Now.Millisecond);
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
        /// Returns the time passed in Milliseconds from the last update;
        /// Enable Utilitiy Update.
        /// </summary>
        /// <returns>Time passed</returns>
        public int DeltaTime(){
            int milliseconds  = (timestamp - prevTimeStamp).Milliseconds;    
            return milliseconds;
        }

        /// <summary>
        /// Updates utilities class for funtionality
        /// </summary>
        public void Update() {
            prevTimeStamp = timestamp;
            timestamp = DateTime.Now;
        }

        /// <summary>
        /// Draws string to the Screen. Like a hud.
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        /// <param name="item">String to draw</param>
        /// <param name="Location">Location on the screen</param>
        public void DrawString(SpriteBatch batch, string item, Vector2 Location)
        {
            Location.X += CameraManager.Instance().GetCamera().Left;
            Location.Y += CameraManager.Instance().GetCamera().TOP;
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
        public T Wrap<T>(T Value, T Min, T Max) where T : System.IComparable<T>
        {
            if (Value.CompareTo(Min) < 0) return Max;
            if (Value.CompareTo(Max) > 0) return Min;
            return Value;
        }

        /// <summary>
        /// Genrates a random number list between min and max ensurin no doubles
        /// </summary>
        /// <param name="Min">Smallest value in the list</param>
        /// <param name="Max">Largest value in the list</param>
        /// <returns></returns>
        public int[] GenerateRandomNumberList(int Min, int Max)
        {
            int count = Math.Abs(Max - Min);
            int[] results = new int[count];
            List<int> numberList = new List<int>();

            for (int index = 0; index < count; index++)
            {
                numberList.Add(Min + index);
            }

            for (int index = 0; index < count; index++)
            {
                int dex = Utilities.Instance().rand.Next(0, numberList.Count);
                results[index] = numberList.ElementAt<int>(dex);
                numberList.RemoveAt(dex);
            }
            return results;
        }

        /// <summary>
        /// Test if Value is in bounds to given range
        /// </summary>
        /// <param name="Value">Value to test</param>
        /// <param name="Min">Minimum Value</param>
        /// <param name="Max">Maximum Value</param>
        public bool InBounds<T>(T Value, T Min, T Max) where T : System.IComparable<T>
        {
            if (Value.CompareTo(Min) < 0) return false;
            if (Value.CompareTo(Max) > 0) return false;
            return true;
        }

    }
}
