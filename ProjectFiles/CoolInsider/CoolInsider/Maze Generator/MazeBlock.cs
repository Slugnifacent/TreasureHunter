using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    public class MazeBlock : GameObject
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Represents each maze block in the game
         */

        public MazeBlock Up;
        public MazeBlock Down;
        public MazeBlock Left;
        public MazeBlock Right;
        public bool Walkable;
        Color color;
        public bool discovered;

        /// <summary>
        /// Constructor
        /// </summary>
        public MazeBlock()
        {
            sprite = TreasureHunter.content.Load<Texture2D>(@"PlanetCute\BrownBlock");
            kinetics = Kinetic.ZERO();
            Kinetic.SetBoundingBoxDimensions(ref kinetics.boundingBox, sprite);
            kinetics.boundingBox.Height -= 40;
            color = Color.Violet;
        }

        /// <summary>
        /// Test if MazeBlock is dead or not
        /// </summary>
        /// <returns>True if dead == true, false otherwise</returns>
        public override bool Dead()
        {
            return false;
        }

        /// <summary>
        /// Unused. Store attack logic here.
        /// </summary>
        public override void Attack()
        {
        }

        /// <summary>
        /// Method for resolving collisions with given Item
        /// </summary>
        /// <param name="Item">Item that has collided with this</param>
        public override void CollisionResolution(GameObject Item)
        {
            if (Item.GetType() == typeof(MazeBlock)) return;
            if (Item.GetType() == typeof(Avatar))
            {
                if (!Walkable)
                {
                    if (Item.kinetics.boundingBox.Bottom >= kinetics.boundingBox.Top && Item.kinetics.boundingBox.Bottom <= kinetics.boundingBox.Top + 5.0f)
                    {
                        float offset = Item.kinetics.boundingBox.Top - Item.kinetics.position.Y;
                        Item.kinetics.position.Y = kinetics.boundingBox.Top - Item.kinetics.boundingBox.Height - offset;
                        Item.kinetics.boundingBox.Y = (int)(Item.kinetics.position.Y + offset);
                        return;
                    }

                    if (Item.kinetics.boundingBox.Right >= kinetics.boundingBox.Left && Item.kinetics.boundingBox.Right <= kinetics.boundingBox.Left + 5.0f)
                    {
                        Item.kinetics.position.X = kinetics.boundingBox.Left - Item.kinetics.boundingBox.Width - 29;
                        Item.kinetics.boundingBox.X = (int)(Item.kinetics.position.X + 29);
                        return;
                    }

                    if (Item.kinetics.boundingBox.Left <= kinetics.boundingBox.Right && Item.kinetics.boundingBox.Left >= kinetics.boundingBox.Right - 5.0f)
                    {
                        if (Item.kinetics.boundingBox.Top <= kinetics.boundingBox.Bottom && Item.kinetics.boundingBox.Top >= kinetics.boundingBox.Bottom - 5.0f)
                        {
                            if (Right != null)
                            {
                                if (!Right.Walkable)
                                {
                                    float offset = Item.kinetics.boundingBox.Top - Item.kinetics.position.Y;
                                    Item.kinetics.position.Y = kinetics.boundingBox.Bottom - offset;
                                    Item.kinetics.boundingBox.Y = (int)(Item.kinetics.position.Y + offset);
                                    Item.kinetics.velocity.Y = 0;
                                    return;
                                }
                            }
                        }
                        Item.kinetics.position.X = kinetics.boundingBox.Right - 29;
                        Item.kinetics.boundingBox.X = (int)(Item.kinetics.position.X + 29);
                        return;

                    }

                    if (Item.kinetics.boundingBox.Top <= kinetics.boundingBox.Bottom && Item.kinetics.boundingBox.Top >= kinetics.boundingBox.Bottom - 5.0f)
                    {
                        float offset = Item.kinetics.boundingBox.Top - Item.kinetics.position.Y;
                        Item.kinetics.position.Y = kinetics.boundingBox.Bottom - offset;
                        Item.kinetics.boundingBox.Y = (int)(Item.kinetics.position.Y + offset);
                        Item.kinetics.velocity.Y = 0;
                        return;
                    }
                }
                else {
                    if (!discovered) {
                        LevelManager.Instance().Discovered();
                        color = Color.White;
                    }
                    discovered = true;
                    
                }
            }
        }


        /// <summary>
        /// Updates the Maze Block
        /// </summary>
        public override void Update()
        {
        }

        /// <summary>
        /// Resets the Maze Block to starting configuration
        /// </summary>
        public void Reset() {
            discovered = false;
            color = Color.Violet;
        }

        /// <summary>
        /// Draws the Maze Block to the screen
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        public override void Draw(SpriteBatch batch)
        {
            if (Walkable)
            {
                if (Vector2.Distance(Avatar.Instance().kinetics.position, kinetics.position) < 600)
                {
                    batch.Draw(sprite, kinetics.position, null, color, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);
                }
                /* Draw Bounding Box
                Vector2 dimensions = new Vector2(kinetics.boundingBox.Width, kinetics.boundingBox.Height);
                Vector2 location = new Vector2(kinetics.boundingBox.Location.X, kinetics.boundingBox.Location.Y);
                batch.Draw(Bar.bar, location, null, color, 0, new Vector2(0, 0), dimensions, SpriteEffects.None, 0);
                */

            }
        }
    }
}
