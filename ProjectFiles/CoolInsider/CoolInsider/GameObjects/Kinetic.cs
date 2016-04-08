using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    public class Kinetic
    {

        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Designed to handle kinimatic information and update them in 
         * a uniform way. boundingBox, velocity, position, orientation etc.
         */

        public Vector2 position;
        public Vector2 prevPosition;
        public Vector2 velocity;
        public Rectangle boundingBox;
        public float maxSpeed;

        /// <summary>
        /// Constructor. Initializes all of the fields to defalt values;
        /// </summary>
        /// <param name="Position">Starting Position</param>
        /// <param name="Velocity">Starting Velocity</param>
        /// <param name="BoundingBox">Starting Bounding Box</param>
        public Kinetic(Vector2 Position, Vector2 Velocity, Rectangle BoundingBox)
        {
            position = Position;
            prevPosition = Position;
            velocity = Velocity;
            boundingBox = BoundingBox;
            maxSpeed = 3;
        }

        /// <summary>
        /// Updates the Kinetic infomration. 
        /// </summary>
        public void Update()
        {
            prevPosition = position;
            position += velocity;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
        }

        /// <summary>
        /// Sets the bounding box size based on the given texture size
        /// </summary>
        /// <param name="box">Bonding Box dimensions to set</param>
        /// <param name="texture">Texture to reference dimensions</param>
        static public void SetBoundingBoxDimensions(ref Rectangle box, Texture2D texture)
        {
            box.Width = texture.Width;
            box.Height = texture.Height;
        }

        /// <summary>
        /// Returns a new Kinetic object;
        /// </summary>
        static public Kinetic ZERO()
        {
            return new Kinetic(Vector2.Zero, Vector2.Zero, new Rectangle());
        }
    }
}
