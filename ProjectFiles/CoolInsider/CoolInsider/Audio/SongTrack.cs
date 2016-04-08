using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace TreasureHunter
{
    class SongTrack : Sound
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Interface for creating background music
         * 
         */

        Song song;

        /// <summary>
        /// Constructor. Requires location of file. Uses XNA 4.0 content pipeline.
        /// </summary>
        /// <param name="FileLocation"></param>
        public SongTrack(string FileLocation)
        {
            song = TreasureHunter.content.Load<Song>(FileLocation);
        }

        /// <summary>
        /// Plays the song
        /// </summary>
        public override void Play()
        {
            if (!Playing() || Paused())
            {
                MediaPlayer.Play(song);
            }
        }

        /// <summary>
        /// Puases the song
        /// </summary>
        public override void Puase()
        {
            MediaPlayer.Pause();
        }

        /// <summary>
        /// Stops the song
        /// </summary>
        public override void Stop()
        {
            MediaPlayer.Stop();
        }

        /// <summary>
        /// Test if sound is playing
        /// </summary>
        /// <returns>True if sound is playing, false otherwise</returns>
        public override bool Playing()
        {
            return MediaPlayer.State == MediaState.Playing;
        }

        /// <summary>
        /// Test if sound is Paused
        /// </summary>
        /// <returns>True if sound is Paused, false otherwise</returns>
        public override bool Paused()
        {
            return MediaPlayer.State == MediaState.Paused;
        }

        /// <summary>
        /// Test if sound is finished playing
        /// </summary>
        /// <returns>True if sound is finished, false otherwise</returns>
        public override bool Finished()
        {
            return MediaPlayer.State == MediaState.Stopped;
        }
    }
}
