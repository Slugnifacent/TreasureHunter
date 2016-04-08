using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CoolIslander
{
    class Physics
    {

        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Manages collision interactions with other objects
         * 
         */
        
        static Physics physics;
        float gravity = .5f;

        Physics() { }

        public static Physics Instance()
        {
            if (physics == null) physics = new Physics();
            return physics;
        }


        // Item Collides with Etem. Resolution Occours on Item
        public bool BoundingBoxCollision(ref Kinetic item, ref Kinetic etem, ref Vector2 result)
        {
            bool collision = false;
            result.X = item.position.X;
            result.Y = item.position.Y;
            if (item.boundingBox.Intersects(etem.boundingBox)) {
                if (item.boundingBox.Right >= etem.boundingBox.Left && item.boundingBox.Right <= etem.boundingBox.Left + 10.0f)
                {
                   result.X = etem.position.X - item.boundingBox.Width;
                   collision = true;
                }
                else if (item.boundingBox.Left <= etem.boundingBox.Right && item.boundingBox.Left >= etem.boundingBox.Right - 10.0f)
                {
                    result.X = etem.boundingBox.Right;
                   collision = true;
                }

                if (item.boundingBox.Bottom >= etem.boundingBox.Top && item.boundingBox.Bottom <= etem.boundingBox.Top + 20.0f)
                {
                    result.Y = etem.boundingBox.Top - item.boundingBox.Height;
                    collision = true;
                }
                else if (item.boundingBox.Top <= etem.boundingBox.Bottom && item.boundingBox.Top >= etem.boundingBox.Bottom - 20.0f)
                {
                    result.Y = etem.boundingBox.Bottom;
                    collision = true;
                }
            }
            return collision;
        }
    }
}
