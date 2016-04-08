using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TreasureHunter
{
    class Movement
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Class that represents basic movement logic found in the game
         */

        /// <summary>
        /// Approaches a point with the given speed.
        /// </summary>
        /// <param name="Location">Current Location</param>
        /// <param name="Destination">Destination</param>
        /// <param name="Movement">Speed at which to get to Destination</param>
        /// <returns>Direction Vector</returns>
        public static float Approach(float Location, float Destination, float Movement)
        {
            return (Destination - Location) * Movement;
        }

        /// <summary>
        /// Approaches a point with the given speed.
        /// </summary>
        /// <param name="Location">Current Location</param>
        /// <param name="Destination">Destination</param>
        /// <param name="Movement">Speed at which to get to Destination</param>
        /// <returns>Direction Vector</returns>
        public static Vector2 Approach(Vector2 Location, Vector2 Destination, float Movement)
        {
            return (Destination - Location) * Movement;
        }

        /// <summary>
        /// Approaches a point with the given speed.
        /// </summary>
        /// <param name="Location">Current Location</param>
        /// <param name="Destination">Destination</param>
        /// <param name="Movement">Speed at which to get to Destination</param>
        /// <returns>Direction Vector</returns>
        public static Vector3 Approach(Vector3 Location, Vector3 Destination, float Movement)
        {
            return (Destination - Location) * Movement;
        }

        /// <summary>
        /// Approaches a point with the given speed.
        /// </summary>
        /// <param name="Location">Current Location</param>
        /// <param name="Destination">Destination</param>
        /// <param name="Movement">Speed at which to get to Destination</param>
        /// <returns>Direction Vector</returns>
        public static Vector4 Approach(Vector4 Location, Vector4 Destination, float Movement)
        {
            return (Destination - Location) * Movement;
        }

        /// <summary>
        /// Seek a point with the given speed.
        /// </summary>
        /// <param name="Location">Current Location</param>
        /// <param name="Destination">Destination</param>
        /// <param name="Movement">Speed at which to get to Destination</param>
        /// <returns>Direction Vector</returns>
        public static Vector2 Seek(Vector2 Location, Vector2 Destination, float Speed)
        {
            Vector2 temp = (Destination - Location);
            temp.Normalize();
            return temp * Speed;
        }

        /// <summary>
        /// Seek a point with the given speed.
        /// </summary>
        /// <param name="Location">Current Location</param>
        /// <param name="Destination">Destination</param>
        /// <param name="Movement">Speed at which to get to Destination</param>
        /// <returns>Direction Vector</returns>
        public static Vector3 Seek(Vector3 Location, Vector3 Destination, float Speed)
        {
            Vector3 temp = (Destination - Location);
            temp.Normalize();
            return temp * Speed;
        }

        /// <summary>
        /// Seek a point with the given speed.
        /// </summary>
        /// <param name="Location">Current Location</param>
        /// <param name="Destination">Destination</param>
        /// <param name="Movement">Speed at which to get to Destination</param>
        /// <returns>Direction Vector</returns>
        public static Vector4 Seek(Vector4 Location, Vector4 Destination, float Speed)
        {
            Vector4 temp = (Destination - Location);
            temp.Normalize();
            return temp * Speed;
        }
    }
}
