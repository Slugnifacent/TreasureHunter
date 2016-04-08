using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoolIslander
{
    class FireBall : DamageBox
    {
        public FireBall(Vector2 Position, Vector2 Velocity, int Time, bool Friendly)
                 : base(Position, Velocity, Time, Friendly)
        {

        }
    }
}
