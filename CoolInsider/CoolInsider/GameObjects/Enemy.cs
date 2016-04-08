using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    public class Enemy : GameObject
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Class that represents the Enemies of the game
         * 
         */

        MazeBlock currentBlock;
        MazeBlock startBlock;
        Vector2 target;
        int choice;

        /// <summary>
        /// Constructor. Places the enemy at the given Mazeblock. The 
        /// mazeblock should be walkable
        /// </summary>
        /// <param name="StartBlock">Starting block</param>
        public Enemy(MazeBlock StartBlock)
        {

            kinetics = Kinetic.ZERO();
            kinetics.position = new Vector2(StartBlock.kinetics.boundingBox.Center.X, StartBlock.kinetics.boundingBox.Center.Y);
            sprite = TreasureHunter.content.Load<Texture2D>(@"PlanetCute\Enemy Bug Small");
            Kinetic.SetBoundingBoxDimensions(ref kinetics.boundingBox, sprite);
            currentBlock = StartBlock;
            target.X = currentBlock.kinetics.boundingBox.Center.X;
            target.Y = currentBlock.kinetics.boundingBox.Center.Y;
            startBlock = StartBlock;
            color = Color.White;
        }

        /// <summary>
        /// Test if Enemy is dead or not
        /// </summary>
        /// <returns>True if dead == true, false otherwise</returns>
        public override bool Dead()
        {
            return false;
        }

        /// <summary>
        /// Updates the Enemy
        /// </summary>
        public override void Update()
        {
            if (Vector2.Distance(Avatar.Instance().kinetics.position, kinetics.position) > 600)
            {
                return;
            }
            if (Vector2.Distance(target, kinetics.position) < 5) {
                MakeDecision();
            }
            kinetics.position += Movement.Seek(kinetics.position, target, 2);
            base.Update();
        }

        /// <summary>
        /// Algorithm that decides where the enemy should move to next.
        /// </summary>
        void MakeDecision()
        {
            MazeBlock temp = null;
            if (Utilities.Instance().rand.NextDouble() > .9f)
            {
                temp = Choose(choice);
            }

            while (temp == null)
            {
                int tempChoice = Utilities.Instance().rand.Next(0, 3);
                if (tempChoice == choice) {
                   
                }
                temp = ChooseClosestToPlayer();
            }
            currentBlock = temp;
            target.X = currentBlock.kinetics.boundingBox.Center.X - sprite.Width/2;
            target.Y = currentBlock.kinetics.boundingBox.Center.Y - sprite.Height/2; ;
        }

        /// <summary>
        /// Algorithm that chooses the next Maze Block that is nearest to the player.
        /// </summary>
        /// <returns>Maze Block that is nearest to the player</returns>
        MazeBlock ChooseClosestToPlayer() {
            MazeBlock temp = null;
            float distance = 10000;
            if (currentBlock.Up != null)
            {
                if (currentBlock.Up.Walkable)
                {
                    float tempDistance = Vector2.Distance(currentBlock.Up.kinetics.position, Avatar.Instance().kinetics.position);
                    if (tempDistance < distance){
                        distance = tempDistance;
                        temp = currentBlock.Up;
                    }
                }
            }
            if (currentBlock.Down != null)
            {
                if (currentBlock.Down.Walkable)
                {
                    float tempDistance = Vector2.Distance(currentBlock.Down.kinetics.position, Avatar.Instance().kinetics.position);
                    if (tempDistance < distance)
                    {
                        distance = tempDistance;
                        temp = currentBlock.Down;
                    }
                }
            }
            if (currentBlock.Left != null)
            {
                if (currentBlock.Left.Walkable)
                {
                    float tempDistance = Vector2.Distance(currentBlock.Left.kinetics.position, Avatar.Instance().kinetics.position);
                    if (tempDistance < distance)
                    {
                        distance = tempDistance;
                        temp = currentBlock.Left;
                    }
                }
            }
            if (currentBlock.Right != null)
            {
                if (currentBlock.Right.Walkable)
                {
                    float tempDistance = Vector2.Distance(currentBlock.Right.kinetics.position, Avatar.Instance().kinetics.position);
                    if (tempDistance < distance)
                    {
                        distance = tempDistance;
                        temp = currentBlock.Right;
                    }
                }
            }
            return temp;
        }

        /// <summary>
        /// Returns Maze block base on int betwenn 0 and 3
        /// </summary>
        /// <param name="Choice">Integer between 0 and 3</param>
        /// <returns>Maze Block based on choice</returns>
        MazeBlock Choose(int Choice) {
            choice = (int)Utilities.Instance().Wrap(Choice, 0, 3);
            switch (choice)
            {
                case 0:
                    if (currentBlock.Up != null)
                    {
                        if (currentBlock.Up.Walkable)
                        {
                            return currentBlock.Up;
                        }
                    }
                    break;
                case 1:
                    if (currentBlock.Down != null)
                    {
                        if (currentBlock.Down.Walkable)
                        {
                            return currentBlock.Down;
                        }
                    }
                    break;
                case 2:
                    if (currentBlock.Left != null)
                    {
                        if (currentBlock.Left.Walkable)
                        {
                            return currentBlock.Left;
                        }
                    }
                    break;
                case 3:
                    if (currentBlock.Right != null)
                    {
                        if (currentBlock.Right.Walkable)
                        {
                            return currentBlock.Right;
                        }
                    }
                    break;
            }
            return null;
        }

        /// <summary>
        /// Resets to initial settings
        /// </summary>
        public override void Reset()
        {
            currentBlock = startBlock;
            target.X = currentBlock.kinetics.boundingBox.Center.X;
            target.Y = currentBlock.kinetics.boundingBox.Center.Y;
            kinetics.position = target;
        }

        /// <summary>
        /// Unused. Store attack logic here.
        /// </summary>
        public override void Attack()
        {

        }

        public override void Draw(SpriteBatch batch)
        {
            if (Vector2.Distance(Avatar.Instance().kinetics.position, kinetics.position) < 600)
                base.Draw(batch);
        }

        /// <summary>
        /// Method for resolving collisions with given Item
        /// </summary>
        /// <param name="Item">Item that has collided with this</param>
        public override void CollisionResolution(GameObject Item)
        {
        }
    }
}
