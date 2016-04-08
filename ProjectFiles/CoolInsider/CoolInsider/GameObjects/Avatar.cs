using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace TreasureHunter
{
    class Avatar : GameObject
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Class the represent the hero of the game
         * 
         */
        static Avatar avatar;
        Rectangle levelBounds;
        bool dead;
        Color color;

        Timer hurtTimer;
        public int MazeAdjustment;

        /// <summary>
        /// Constructor
        /// </summary>
        Avatar()
        {
            color = Color.White;
            hurtTimer = new Timer(.2f);
            hurtTimer.Puase();
            sprite = TreasureHunter.content.Load<Texture2D>(@"PlanetCute\Character Horn Girl");
            kinetics = new Kinetic(new Vector2(400, 400), new Vector2(0, 0), new Rectangle());
            kinetics.boundingBox.Width  = 31;
            kinetics.boundingBox.Height = 20;
            kinetics.boundingBox.Offset(29,60);
            kinetics.maxSpeed = 5;

            MazeAdjustment = 0;
        }

        /// <summary>
        /// Returns the singleton instance of this object
        /// </summary>
        /// <returns>Returns the static Avatar instance</returns>
        public static Avatar Instance()
        {
            if (avatar == null)
            {
                avatar = new Avatar();
            }
            return avatar;
        }

        /// <summary>
        /// Test if Avatar is dead or not
        /// </summary>
        /// <returns>True if dead == true, false otherwise</returns>
        public override bool Dead()
        {
            return dead;
        }

        /// <summary>
        /// Updates the Avatar
        /// </summary>
        public override void Update()
        {
            MazeAdjustment = 0;
            float speed = 5;
            hurtTimer.Update();
            if(hurtTimer.Ready()){
                color = Color.White;
                hurtTimer.Reset();
                hurtTimer.Puase();
            }
            kinetics.velocity.X = ControllerInput.Instance().Sticks.Left.X * speed;
            kinetics.velocity.Y = -ControllerInput.Instance().Sticks.Left.Y * speed;

            if (ControllerInput.Instance().GetKey(Keys.Left).Held)
            {
                kinetics.velocity.X = -speed;
            }

            if (ControllerInput.Instance().GetKey(Keys.Right).Held)
            {
                kinetics.velocity.X = speed;
            }

            if (ControllerInput.Instance().GetKey(Keys.Down).Held)
            {
                kinetics.velocity.Y = speed;
            }

            if (ControllerInput.Instance().GetKey(Keys.Up).Held)
            {
                kinetics.velocity.Y = -speed;
            }

            Attack();

            kinetics.Update();
            kinetics.boundingBox.Offset(29, 60);

        }

        /// <summary>
        /// Collision Resolution. Called when this collides with another object
        /// </summary>
        /// <param name="Item">Item that has collided with this</param>
        public override void CollisionResolution(GameObject Item)
        {
            if (Item.GetType() == typeof(Enemy))
            {
                dead = true;
            }
        }

        /// <summary>
        /// Unused. Designed to contain attack logic
        /// </summary>
        public override void Attack()
        {

        }

        /// <summary>
        /// Revives the avatar. Dead() == true.
        /// </summary>
        public void Revive()
        {
            dead = false;
        }

        /// <summary>
        /// Kills the avatar. Dead() == false.
        /// </summary>
        public void Kill() {
            dead = true;
        }
    }
}
