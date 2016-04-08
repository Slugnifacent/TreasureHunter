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
                    float margin = (float)Math.Ceiling(Avatar.Instance().speed) + 1;
                    //Down
                    if (Item.kinetics.boundingBox.Bottom >= kinetics.boundingBox.Top && Item.kinetics.boundingBox.Bottom <= kinetics.boundingBox.Top + margin)
                    {
                        if (ControllerInput.Instance().GetKey(Microsoft.Xna.Framework.Input.Keys.Space).Pressed)
                        {
                            MazeBlock target = Down;
                            if (target.Walkable == true)
                            {
                                Item.kinetics.position.X = target.kinetics.position.X + 10;
                                Item.kinetics.position.Y = target.kinetics.position.Y - 25;

                                Item.kinetics.boundingBox.X = (int)target.kinetics.position.X + Item.kinetics.BoundingBoxOffset.X;
                                Item.kinetics.boundingBox.Y = (int)target.kinetics.position.Y + Item.kinetics.BoundingBoxOffset.Y;
                                Avatar.Instance().Teleported();
                            }
                        }
                        else
                        {
                            float offset = Item.kinetics.boundingBox.Top - Item.kinetics.position.Y;
                            Item.kinetics.position.Y = kinetics.boundingBox.Top - Item.kinetics.boundingBox.Height - Item.kinetics.BoundingBoxOffset.Y;
                            Item.kinetics.boundingBox.Y = (int)(Item.kinetics.position.Y + Item.kinetics.BoundingBoxOffset.Y);
                        }
                        return;
                    }

                    //Left
                    if (Item.kinetics.boundingBox.Right >= kinetics.boundingBox.Left && Item.kinetics.boundingBox.Right <= kinetics.boundingBox.Left + margin)
                    {
                        if (ControllerInput.Instance().GetKey(Microsoft.Xna.Framework.Input.Keys.Space).Pressed)
                        {
                            MazeBlock target = Right;
                            if (target.Walkable == true)
                            {
                                Item.kinetics.position.X = target.kinetics.position.X + 10;
                                Item.kinetics.position.Y = target.kinetics.position.Y - 25;

                                Item.kinetics.boundingBox.X = (int)target.kinetics.position.X + Item.kinetics.BoundingBoxOffset.X;
                                Item.kinetics.boundingBox.Y = (int)target.kinetics.position.Y + Item.kinetics.BoundingBoxOffset.Y;
                                Avatar.Instance().Teleported();
                            }
                        }
                        else
                        {
                            Item.kinetics.position.X = kinetics.boundingBox.Left - Item.kinetics.boundingBox.Width - Item.kinetics.BoundingBoxOffset.X;
                            Item.kinetics.boundingBox.X = (int)(Item.kinetics.position.X + Item.kinetics.BoundingBoxOffset.X);
                            return;
                        }
                    }

                    if (Item.kinetics.boundingBox.Left <= kinetics.boundingBox.Right && Item.kinetics.boundingBox.Left >= kinetics.boundingBox.Right - margin)
                    {
                        if (Item.kinetics.boundingBox.Top <= kinetics.boundingBox.Bottom && Item.kinetics.boundingBox.Top >= kinetics.boundingBox.Bottom - margin)
                        {
                            if (Right != null)
                            {
                                if (!Right.Walkable)
                                {
                                    if (ControllerInput.Instance().GetKey(Microsoft.Xna.Framework.Input.Keys.Space).Pressed)
                                    {
                                        MazeBlock target = Up;
                                        if (target.Walkable == true)
                                        {
                                            Item.kinetics.position.X = target.kinetics.position.X + 10;
                                            Item.kinetics.position.Y = target.kinetics.position.Y - 25;

                                            Item.kinetics.boundingBox.X = (int)target.kinetics.position.X + Item.kinetics.BoundingBoxOffset.X;
                                            Item.kinetics.boundingBox.Y = (int)target.kinetics.position.Y + Item.kinetics.BoundingBoxOffset.Y;
                                            Avatar.Instance().Teleported();
                                        }
                                    }
                                    else
                                    {
                                        float offset = Item.kinetics.boundingBox.Top - Item.kinetics.position.Y;
                                        Item.kinetics.position.Y = kinetics.boundingBox.Bottom - Item.kinetics.BoundingBoxOffset.Y;
                                        Item.kinetics.boundingBox.Y = (int)(Item.kinetics.position.Y + Item.kinetics.BoundingBoxOffset.Y);
                                        Item.kinetics.velocity.Y = 0;
                                        return;
                                    }
                                }
                            }
                        }

                        if (ControllerInput.Instance().GetKey(Microsoft.Xna.Framework.Input.Keys.Space).Pressed)
                        {
                            MazeBlock target = Left;
                            if (target.Walkable == true)
                            {
                                Item.kinetics.position.X = target.kinetics.position.X + 10;
                                Item.kinetics.position.Y = target.kinetics.position.Y - 25;

                                Item.kinetics.boundingBox.X = (int)target.kinetics.position.X + Item.kinetics.BoundingBoxOffset.X;
                                Item.kinetics.boundingBox.Y = (int)target.kinetics.position.Y + Item.kinetics.BoundingBoxOffset.Y;
                                Avatar.Instance().Teleported();
                            }
                        }
                        else
                        {
                            Item.kinetics.position.X = kinetics.boundingBox.Right - Item.kinetics.BoundingBoxOffset.X;
                            Item.kinetics.boundingBox.X = (int)(Item.kinetics.position.X + Item.kinetics.BoundingBoxOffset.X);

                        }
                        return;
                    }

                    if (Item.kinetics.boundingBox.Top <= kinetics.boundingBox.Bottom && Item.kinetics.boundingBox.Top >= kinetics.boundingBox.Bottom - margin)
                    {
                        if (ControllerInput.Instance().GetKey(Microsoft.Xna.Framework.Input.Keys.Space).Pressed)
                        {
                            MazeBlock target = Up;
                            if (target.Walkable == true)
                            {
                                Item.kinetics.position.X = target.kinetics.position.X + 10;
                                Item.kinetics.position.Y = target.kinetics.position.Y - 25;

                                Item.kinetics.boundingBox.X = (int)target.kinetics.position.X + Item.kinetics.BoundingBoxOffset.X;
                                Item.kinetics.boundingBox.Y = (int)target.kinetics.position.Y + Item.kinetics.BoundingBoxOffset.Y;
                                Avatar.Instance().Teleported();
                            }
                        }
                        else
                        {
                            float offset = Item.kinetics.boundingBox.Top - Item.kinetics.position.Y;
                            Item.kinetics.position.Y = kinetics.boundingBox.Bottom - Item.kinetics.BoundingBoxOffset.Y;
                            Item.kinetics.boundingBox.Y = (int)(Item.kinetics.position.Y + Item.kinetics.BoundingBoxOffset.Y);
                            Item.kinetics.velocity.Y = 0;
                        }
                        return;
                    }
                }
                else
                {
                    if (!discovered)
                    {
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
        public override void Reset()
        {
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
