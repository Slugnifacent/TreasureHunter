using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TreasureHunter
{
    public class Timer
    {

        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * A Stopwatch class
         * 
         */

        float goalTime;
        float currentTime;
        bool puase;
        DateTime timeStamp;
        bool reverse;

        /// <summary>
        /// Amount of time. Counts upward from Zero
        /// </summary>
        /// <param name="Seconds">Amount of time in seconds</param>
        public Timer(float Seconds)
        {
            goalTime = Seconds;
            timeStamp = DateTime.Now;
            currentTime = 0;
            reverse = false;
        }

        /// <summary>
        /// Amount of time. Counts upward from Zero
        /// </summary>
        /// <param name="Seconds">Amount of time in seconds</param>
        /// <param name="Reverse">If true, Counts from given Seconds to 0</param>
        public Timer(float Seconds, bool Reverse)
        {
            goalTime = Seconds;
            timeStamp = DateTime.Now;
            currentTime = 0;
            reverse = Reverse;
        }

        /// <summary>
        /// Updates the timer
        /// </summary>
        public void Update() {
            if (puase) return;
            DateTime time = DateTime.Now;
            currentTime = (float)DateTime.Now.Subtract(timeStamp).TotalSeconds;
            if (Ready()) {
                Puase();
                currentTime = goalTime;
            }
        }

        /// <summary>
        /// Returns the Time as float between 0 and 1;
        /// </summary>
        /// <returns></returns>
        public float Nomalized() {
            return currentTime / goalTime;
        }

        /// <summary>
        /// True if goal has been met
        /// </summary>
        /// <returns>True if goal has been met, false otherwise</returns>
        public bool  Ready() { 
            return currentTime >= goalTime;
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void Start() {
            puase = false;
        }

        /// <summary>
        /// Puases the timer
        /// </summary>
        public void Puase() {
            puase = true;
        }

        /// <summary>
        /// Resets the timer
        /// </summary>
        public void Reset() {
            currentTime = 0;
            timeStamp = DateTime.Now;
        }

        /// <summary>
        /// Grabs current time of the Timer.
        /// </summary>
        /// <returns>Current time on the timer.</returns>
        public float CurrentTime()
        {
            if (reverse) {
                return goalTime - currentTime;
            }
            return currentTime;
        }
    }
}
