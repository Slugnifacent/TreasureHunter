using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CoolIslander
{
    class Bar
    {
        public static Texture2D bar;

        double health, healthcap;
        float unitHealth;

        float angle;

        Vector2 position;

        Vector2 dimensions;
        Vector2 dimensionCap;

        Color color;
        Boolean Horizontal;


        public Bar(ContentManager Content, double health, Vector2 location, int width, int height, Boolean Horizontal)
        {
            this.health = health;
            this.healthcap = health;
            position = location;
            dimensionCap = new Vector2(width, height);
            dimensions = new Vector2(width, height);
            this.Horizontal = Horizontal;
            angle = 0;
            if (Horizontal)
            {
                unitHealth = dimensions.X / (float)health;
            }
            else
            {
                unitHealth = dimensions.Y / (float)health;
            }
            color = Color.Violet;
            bar = Content.Load<Texture2D>(@"pixel");
        }

        public Bar(ContentManager Content, double health, Vector2 location, int width, int height, Color color, Boolean Horizontal)
        {
            this.health = health;
            this.healthcap = health;
            position = location;
            dimensionCap = new Vector2(width, height);
            dimensions = new Vector2(width, height);
            this.Horizontal = Horizontal;
            this.color = color;
            angle = 0;
            if (Horizontal)
            {
                unitHealth = dimensions.X / (float)health;
            }
            else
            {
                unitHealth = dimensions.Y / (float)health;
            }
            bar = Content.Load<Texture2D>(@"orange");
        }

        public void draw(SpriteBatch batch)
        {
            Vector3 translation = Islander.Cam.View().Translation;
            Vector2 temp = Vector2.Zero;
            temp.X -= translation.X;
            temp.Y -= translation.Y;

            batch.Draw(bar, position + temp, null, color, angle, new Vector2(0, 0), dimensions, SpriteEffects.None, 0);
        }

        public void setHealth(float newHealth)
        {
            this.health = newHealth;
            if (Horizontal)
            {
                dimensions.X = unitHealth * newHealth;
            }
            else
            {
                dimensions.Y = unitHealth * newHealth;
            }
        }

        public void grow(float growth)
        {
            if (health + growth < healthcap)
            {
                health += growth;
                if (Horizontal)
                {
                    dimensions.X += unitHealth * growth;
                }
                else
                {
                    dimensions.Y += unitHealth * growth;
                }
            }
            else
            {
                health = healthcap;
                if (Horizontal)
                {
                    dimensions.X = unitHealth * (float)healthcap;
                }
                else
                {
                    dimensions.Y = unitHealth * (float)healthcap;
                }
            }
        }

        public void decrease(float reduction)
        {
            if (health - reduction > 0)
            {
                health -= reduction;
                if (Horizontal)
                {
                    dimensions.X -= unitHealth * reduction;
                }
                else
                {
                    dimensions.Y -= unitHealth * reduction;
                }
            }
            else
            {
                health = 0;
                if (Horizontal)
                {
                    dimensions.X = 0;
                }
                else
                {
                    dimensions.Y = 0;
                }
            }
        }

        public void modHealth(float health)
        {
            this.health = health;
            this.healthcap = health;
            dimensions = dimensionCap;
            if (Horizontal)
            {
                unitHealth = dimensions.X / (float)health;
            }
            else
            {
                unitHealth = dimensions.Y / (float)health;
            }
        }

        public Color getColor()
        {
            return color;
        }

        public void modColor(Color color)
        {
            this.color = color;
        }

        public void modPosition(Vector2 location)
        {
            position = location;
        }

        public void modAngle(float angle)
        {
            this.angle = angle;
        }

        public void fade() {
            int check = this.color.B;
            if (check + 1 < 255) this.color.B += 3;
            else this.color.B = 255;
        }

        public float Health (){
            return (float)health;
        }
    }
}
