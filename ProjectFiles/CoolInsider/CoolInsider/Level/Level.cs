using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    public class Level
    {

        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Class that represents the Levels of the game
         */

        int width;
        int height;
        int scaleX;
        int scaleY;
        int scaledWidth;
        int scaledHeight;
        public Timer timeLeft;
        public Vector2 firstPosition;
        public List<GameObject> ObjectList;
        public Treasure treasure;
        public bool complete;
        public Radar radar;
        public float walkableSpace;
        public float discovered;
        public int death;
        public bool timerStarted;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Width">Unit width of the maze</param>
        /// <param name="Height">Unit height of the maze</param>
        /// <param name="ScaleX">Unit width of maze cell</param>
        /// <param name="ScaleY">Unit height of maze cell</param>
        /// <param name="Seconds">Time required to complete</param>
        public Level(int Width, int Height, int ScaleX, int ScaleY, int Seconds)
        {
            width = Width;
            height = Height;
            scaleX = ScaleX;
            scaleY = ScaleY;
            discovered = 0;
            death = 0;
            MazeGenerator levelRef = new MazeGenerator(width, height, ScaleX, ScaleY);
            scaledWidth = levelRef.ScaledWidth();
            scaledHeight = levelRef.ScaledHeight();
            ObjectList = new List<GameObject>();
   
            List<MazeBlock> walkables = new List<MazeBlock>();
            radar = new Radar(levelRef.maze);
            
            foreach (GameObject Obj in levelRef.maze)
            {
                MazeBlock temp = Obj as MazeBlock;
                if (temp.Walkable)
                {
                    walkables.Add(temp);
                    walkableSpace++;
                }
                InsertGameObject(Obj);
            }



            firstPosition = walkables.ElementAt<MazeBlock>(0).kinetics.position;
            treasure = new Treasure(walkables.ElementAt<MazeBlock>(walkables.Count - 1));
            InsertGameObject(treasure);
            walkables.RemoveAt(0);
           
            Random rand = new Random(DateTime.Now.Millisecond);
            if (walkables.Count > 0)
            {
                walkables.RemoveAt(walkables.Count - 1);
                int index = walkables.Count / 10;
                while (index > 0)
                {
                    MazeBlock block = walkables.ElementAt<MazeBlock>(rand.Next(0, walkables.Count));
                    if (Vector2.Distance(firstPosition, block.kinetics.position) > 500)
                    {
                        InsertGameObject(new Enemy(block));
                        index--;
                    }
                }
            }

            timeLeft = new Timer(Seconds, true);
            
        }

        /// <summary>
        /// Updates the level state
        /// </summary>
        public void Update()
        {
            if (!timerStarted) {
                timeLeft.Reset();
                timeLeft.Start();
                timerStarted = true;
            }
            for (int index = 0; index < ObjectList.Count(); index++)
            {
                GameObject current = ObjectList.ElementAt<GameObject>(index);
                if (current.Dead())
                {
                    ObjectList.RemoveAt(index);
                    index--;
                }
                else
                {
                    current.Update();
                }
            }
            Collision();
            timeLeft.Update();
            if (timeLeft.Ready())
            {
                Avatar.Instance().Kill();
            }
            if (Avatar.Instance().Dead()) {
                    timeLeft.Reset();
                    LevelManager.Instance().GameOver();
                    LevelManager.Instance().GenerateFirstLevel();
            }
        }

        /// <summary>
        /// Inserts game object into leve
        /// </summary>
        /// <param name="Item">Game object to insert</param>
        /// <returns>True if object inserted, false if otherwise</returns>
        public bool InsertGameObject(GameObject Item)
        {
            ObjectList.Add(Item);
            return true;
        }

        /// <summary>
        /// Returs width of the maze in units
        /// </summary>
        /// <returns>Width of maze</returns>
        public int Width()
        {
            return width;
        }

        /// <summary>
        /// Returs height of the maze in units
        /// </summary>
        /// <returns>height of maze</returns>
        public int Height()
        {
            return height;
        }

        /// <summary>
        /// Returs width of the mazeblock unit
        /// </summary>
        /// <returns>width of mazeblock</returns>
        public int ScaleX()
        {
            return scaleX;
        }

        /// <summary>
        /// Returs height of the mazeblock unit
        /// </summary>
        /// <returns>height of mazeblock</returns>
        public int ScaleY()
        {
            return scaleY;
        }

        /// <summary>
        /// Returns full width of the entire maze
        /// </summary>
        /// <returns>Width of entire maze</returns>
        public int ScaledWidth()
        {
            return scaledWidth;
        }

        /// <summary>
        /// Returns full height of the entire maze
        /// </summary>
        /// <returns>Height of entire maze</returns>
        public int ScaledHeight()
        {
            return scaledHeight;
        }

        /// <summary>
        /// Increase counter for newly discovered Mazeblocks
        /// </summary>
        public void NewDiscovery()
        {
            discovered++;
        }

        /// <summary>
        /// Resets the level to its starting configuration
        /// </summary>
        public void Reset()
        {
            discovered = 0;
            timeLeft.Reset();
            timeLeft.Start();
            foreach (GameObject Item in ObjectList)
            {
                if (Item.GetType() == typeof(Enemy))
                {
                    (Item as Enemy).Reset();
                }
                if (Item.GetType() == typeof(MazeBlock))
                {
                    (Item as MazeBlock).Reset();
                }
            }
            death++;
        }

        /// <summary>
        /// Ratio of discovered mazeblocks vs undiscovered blocks
        /// </summary>
        /// <returns>Ratio </returns>
        public float Discovered() {
            return discovered / walkableSpace;
        }

        /// <summary>
        /// Draws the level to the screen
        /// </summary>
        /// <param name="batch">Microsoft sprite batch</param>
        public void Draw(SpriteBatch batch)
        {
            
            foreach (GameObject Obj in ObjectList)
            {
                Obj.Draw(batch);
            }
            
            
            float minutes = (int)timeLeft.CurrentTime() / 60;
            float seconds = (int)(timeLeft.CurrentTime() % 60);
            Utilities.Instance().DrawString(batch, "Time Left!  " + minutes + ":" + seconds, new Vector2(10, 10));
            Utilities.Instance().DrawString(batch, "Level:      " + LevelManager.Instance().CurrentLevel(), new Vector2(10, 30));
        }

        /// <summary>
        /// Collision detection for all obects against the Avatar.
        /// </summary>
        public void Collision()
        {
            for (int index = 0; index < ObjectList.Count; index++)
            {
                GameObject Item = ObjectList.ElementAt<GameObject>(index);
                GameObject Etem = Avatar.Instance();
                if (Vector2.Distance(Item.kinetics.position, Etem.kinetics.position) > 200)
                {
                    continue;
                }
                if (Item.kinetics.boundingBox.Intersects(Etem.kinetics.boundingBox))
                {
                    Item.CollisionResolution(Etem);
                    Etem.CollisionResolution(Item);
                }
            }
        }
    }
}
