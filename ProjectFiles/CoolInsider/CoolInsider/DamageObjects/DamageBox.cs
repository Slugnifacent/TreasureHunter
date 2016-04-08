using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoolIslander
{
    class DamageBox : GameObject
    {
        bool dead;
        int time;
        bool friendly;
        public DamageBox(Vector2 Position, Vector2 Velocity, int Time, bool Friendly)
        {
            sprite = Islander.content.Load<Texture2D>("Hero");
            kinetics = Kinetic.ZERO();
            kinetics.position = Position;
            kinetics.velocity = Velocity;
            Kinetic.SetBoundingBoxDimensions(ref kinetics.boundingBox, sprite);
            dead = false;
            time = Time;
            friendly = Friendly;
        }

        public override void Update()
        {
            if (time <= 0)
            {
                dead = true;
            }
            kinetics.Update();
            time--;
        }

        public override bool Dead()
        {
            return dead;
        }

        public override void CollisionResolution(GameObject Item)
        {
            if (Item.GetType() == typeof(Avatar))
            {
                if (!friendly)
                {
                    dead = true;
                }
            }

            if (Item.GetType() == typeof(Enemy))
            {
                if (friendly)
                {
                    dead = true;
                }
            }
        }

        public override void Attack()
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch batch)
        {

        }


        public bool Friendly()
        {
            return friendly;
        }
    }
}
