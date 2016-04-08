using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace TreasureHunter
{
    class SoundManager
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Central hub for all sounds played in the game
         * 
         */

        static SoundManager manager;
        List<Sound> sounds;
        Sound bgm;

        /// <summary>
        /// Constructor
        /// </summary>
        SoundManager() {
            sounds = new List<Sound>();
        }

        /// <summary>
        /// Returns the singleton instance of this object
        /// </summary>
        /// <returns>Returns the static SoundManager instance</returns>
        public static SoundManager Instance(){
            if (manager == null) {
                manager = new SoundManager();
            }
            return manager;
        }

        /// <summary>
        /// Updates the sounds in the sound manager
        /// </summary>
        public void Update() {
            for (int index = 0; index < sounds.Count; index++) {
                Sound current = sounds.ElementAt<Sound>(index);
                if (!current.Finished())
                {
                   current.Play();
                }
                else {
                    sounds.RemoveAt(index);
                    index--;
                }
            }
        }

        /// <summary>
        /// Adjust the volume of the backgroun music. 
        /// Range{0,1}
        /// </summary>
        /// <param name="Value">Value between 0 and 1</param>
        public void Volume(float Value) {
            MediaPlayer.Volume = Value;
        }

        /// <summary>
        /// Adds a sound to the SoundManager
        /// </summary>
        /// <param name="sound">Sound to add</param>
        public void AddSound(SFX sound) {
            sound.Play();
            sounds.Add(sound);
        }

        /// <summary>
        /// Background music to be played by the sound manager
        /// </summary>
        /// <param name="Track">SongTrack to play</param>
        public void BGM(SongTrack Track)
        {
            bgm = Track;
        }

        /// <summary>
        /// Returns the current background music
        /// </summary>
        /// <returns>Current bakcground music</returns>
        public Sound BGM() {
            return bgm;
        }
    }
}
