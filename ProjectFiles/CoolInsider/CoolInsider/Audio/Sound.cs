using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TreasureHunter
{
    abstract class Sound
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Common interface for all sounds SFX or Songtracks
         */

        /// <summary>
        /// Plays the sound
        /// </summary>
        abstract public void Play();

        /// <summary>
        /// Puases the sound
        /// </summary>
        abstract public void Puase();

        /// <summary>
        /// Stops the sound
        /// </summary>
        abstract public void Stop();

        /// <summary>
        /// Test if sound is playing
        /// </summary>
        /// <returns>True if sound is playing, false otherwise</returns>
        abstract public bool Playing();

        /// <summary>
        /// Test if sound is Paused
        /// </summary>
        /// <returns>True if sound is Paused, false otherwise</returns>
        abstract public bool Paused();

        /// <summary>
        /// Test if sound is finished playing
        /// </summary>
        /// <returns>True if sound is finished, false otherwise</returns>
        abstract public bool Finished();
    }
}
