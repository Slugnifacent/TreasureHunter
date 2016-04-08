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

        Timer hurtTimer;
        Timer teleport;
        public float speed;
        public float standardSpeed;
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
            kinetics.BoundingBoxOffset.X = 29;
            kinetics.BoundingBoxOffset.Y = 60;
            kinetics.maxSpeed = 7;
            teleport = new Timer(.5f);
            teleport.Start();
            speed = standardSpeed = 5;
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

        public void Teleported() {
            teleport.Reset();
            teleport.Start();
            color.A = 0;
            SoundManager.Instance().AddSound(new SFX("SoundEffects\\ichrmova"));
            switch (Utilities.Instance().rand.Next(0, 4)) { 
                case 0:
                    SoundManager.Instance().AddSound(new SFX("SoundEffects\\Already There"));
                    break;
                case 1:
                    SoundManager.Instance().AddSound(new SFX("SoundEffects\\Deconstructing"));
                    break;
                case 2:
                    SoundManager.Instance().AddSound(new SFX("SoundEffects\\I'm Gone"));
                    break;
                case 3:
                    SoundManager.Instance().AddSound(new SFX("SoundEffects\\Never Existed"));
                    break;
                case 4:
                    SoundManager.Instance().AddSound(new SFX("SoundEffects\\Without a Trace"));
                    break;

            }


        }

        /// <summary>
        /// Updates the Avatar
        /// </summary>
        public override void Update()
        {
            

            kinetics.velocity.X = ControllerInput.Instance().Sticks.Left.X * speed;
            kinetics.velocity.Y = -ControllerInput.Instance().Sticks.Left.Y * speed;
            teleport.Update();
            if (teleport.Ready())
            {
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
            }
           color.A = (byte)(255*teleport.Nomalized());
      
           base.Update();
        }

        /// <summary>
        /// Resets to initial settings
        /// </summary>
        public override void Reset()
        {
            teleport.Reset();
            teleport.Start();
            color.A = 1;
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

        public void Draw(SpriteBatch batch, Color _Color)
        {
            color = _Color;
            base.Draw(batch);
            color = Color.White;
        }

        public void PlaceAt(MazeBlock Block) {
            kinetics.position = new Vector2(Block.kinetics.boundingBox.Center.X, Block.kinetics.boundingBox.Center.Y);
        }
    }
}
