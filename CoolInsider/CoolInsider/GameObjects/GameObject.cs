using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    public abstract class GameObject
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Class the represent all game objects 
         * 
         */

        /// <summary>
        /// Sprite drawn to the screen
        /// </summary>
        protected Texture2D sprite;
        public Color color;
        protected bool dead;
        /// <summary>
        /// Contains kinetic information about the object
        /// </summary>
        public Kinetic kinetics;

        /// <summary>
        /// Test if object is dead or not
        /// </summary>
        /// <returns>True if dead == true, false otherwise</returns>
        abstract public bool Dead();

        /// <summary>
        /// Updates the object
        /// </summary>
        virtual public void Update()
        {
            kinetics.Update();
        }

        /// <summary>
        /// Resets the object to initialized settings
        /// </summary>
        abstract public void Reset();

        /// <summary>
        /// Unused. Designed to contain attack logic
        /// </summary>
        abstract public void Attack();

        /// <summary>
        /// Revives the GameObject. 
        /// </summary>
        virtual public void Revive()
        {
            dead = false;
        }

        /// <summary>
        /// Kills the GameObject.
        /// </summary>
        virtual public void Kill()
        {
            dead = true;
        }

        /// <summary>
        /// Collision Resolution. Called when this collides with another object
        /// </summary>
        /// <param name="Item">Item that has collided with this</param>
        abstract public void CollisionResolution(GameObject Item);

        virtual public void Draw(SpriteBatch batch)
        {
            batch.Draw(sprite, kinetics.position, color);
        }
    }
}
