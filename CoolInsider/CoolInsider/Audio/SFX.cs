using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace TreasureHunter
{
    class SFX : Sound
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Interface for creating sound effects in the game
         * 
         */

        SoundEffect sound;
        SoundEffectInstance instance;

        public SFX(string FileLocation) {
            sound = TreasureHunter.content.Load<SoundEffect>(FileLocation);
            instance = sound.CreateInstance();
        }

        /// <summary>
        /// Plays the sound
        /// </summary>
        public override void Play()
        {
            if (!Playing() || Paused())
            {
                instance.Play();
            }
        }

        /// <summary>
        /// Puases the sound
        /// </summary>
        public override void Puase()
        {
            instance.Pause();
        }

        /// <summary>
        /// Stops the sound
        /// </summary>
        public override void Stop()
        {
            instance.Stop();
        }

        /// <summary>
        /// Test if sound is playing
        /// </summary>
        /// <returns>True if sound is playing, false otherwise</returns>
        public override bool Playing()
        {
            return instance.State == SoundState.Playing;
        }

        /// <summary>
        /// Test if sound is Paused
        /// </summary>
        /// <returns>True if sound is Paused, false otherwise</returns>
        public override bool Paused()
        {
            return instance.State == SoundState.Paused;
        }

        /// <summary>
        /// Test if sound is finished playing
        /// </summary>
        /// <returns>True if sound is finished, false otherwise</returns>
        public override bool Finished()
        {
            return instance.State == SoundState.Stopped;
        } 
    }
}
