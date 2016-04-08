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

        int unitWidth;
        int unitHeight;
        int cellWidth;
        int cellHeight;
        int levelWidth;
        int levelHeight;
        public int MaxCombo;
        int combo;

        public float discovered;
        public float walkableSpace;

        public Timer timeLeft;
        public Timer comboTimer;
        public Vector2 firstPosition;
        public List<GameObject> UpdateList;
        public List<GameObject> DrawList;
        public Treasure treasure;
        public bool complete;
        public Radar radar;
        

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
            unitWidth = Width;
            unitHeight = Height;
            cellWidth = ScaleX;
            cellHeight = ScaleY;
            combo = 0;
            MaxCombo = 0;
            discovered = 0;
            death = 0;
            List<MazeBlock> walkables = new List<MazeBlock>();

            UpdateList = new List<GameObject>();
            UpdateList = new List<GameObject>();
            DrawList = new List<GameObject>();

            while (walkables.Count() < 1)
            {
                MazeGenerator levelRef = new MazeGenerator(unitWidth, unitHeight, ScaleX, ScaleY);
                walkables = new List<MazeBlock>();
                UpdateList = new List<GameObject>();
                radar = new Radar(levelRef.maze);
                levelWidth = levelRef.MazeWidth();
                levelHeight = levelRef.MazeHeight();

                foreach (GameObject Obj in levelRef.maze)
                {
                    if (Obj == null) continue;
                    MazeBlock temp = Obj as MazeBlock;
                    if (temp.Walkable)
                    {
                        walkables.Add(temp);
                        walkableSpace++;
                        InsertGameObjectDraw(Obj);
                    }
                    InsertGameObject(Obj);
                }
            }

            firstPosition = walkables.ElementAt<MazeBlock>(0).kinetics.position;
            treasure = new Treasure(walkables.ElementAt<MazeBlock>(walkables.Count - 1));
            InsertGameObject(treasure);
            walkables.RemoveAt(0);

            PlaceEnemies(walkables, 10);
            timeLeft = new Timer(Seconds, true);
            comboTimer = new Timer(1);
        }

        void PlaceEnemies(List<MazeBlock> Walkables, int Count)
        {
            if (Walkables.Count > 0)
            {
                Walkables.RemoveAt(Walkables.Count - 1);
                while (Count > 0)
                {
                    MazeBlock block = Walkables.ElementAt<MazeBlock>(Utilities.Instance().rand.Next(0, Walkables.Count));
                    if (Vector2.Distance(firstPosition, block.kinetics.position) > 500)
                    {
                        InsertGameObject(new Enemy(block));
                    }
                    Count--;
                }
            }
        }


        /// <summary>
        /// Updates the level state
        /// </summary>
        public void Update()
        {
            Avatar.Instance().speed = Avatar.Instance().standardSpeed + Avatar.Instance().standardSpeed * (combo / 50.0f);
            Avatar.Instance().speed = MathHelper.Clamp(Avatar.Instance().speed, 0, Avatar.Instance().kinetics.maxSpeed);

            comboTimer.Update();
            if (!timerStarted) {
                timeLeft.Reset();
                timeLeft.Start();
                timerStarted = true;
            }
            for (int index = 0; index < UpdateList.Count(); index++)
            {
                GameObject current = UpdateList.ElementAt<GameObject>(index);
                if (current.Dead())
                {
                    UpdateList.RemoveAt(index);
                    index--;
                }
                else
                {
                    current.Update();
                    if (current != Avatar.Instance())
                    {
                        Collision(current, Avatar.Instance());
                    }
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
            if (comboTimer.Ready()) {
                combo = 0;
            }
        }

        /// <summary>
        /// Inserts game object into leve
        /// </summary>
        /// <param name="Item">Game object to insert</param>
        /// <returns>True if object inserted, false if otherwise</returns>
        public bool InsertGameObject(GameObject Item)
        {
            InsertGameObjectUpdate(Item);
            InsertGameObjectDraw(Item);
            return true;
        }
        /// <summary>
        /// Inserts game object into leve
        /// </summary>
        /// <param name="Item">Game object to insert</param>
        /// <returns>True if object inserted, false if otherwise</returns>
        public bool InsertGameObjectDraw(GameObject Item)
        {
            DrawList.Add(Item);
            return true;
        }

        /// <summary>
        /// Inserts game object into leve
        /// </summary>
        /// <param name="Item">Game object to insert</param>
        /// <returns>True if object inserted, false if otherwise</returns>
        public bool InsertGameObjectUpdate(GameObject Item)
        {
            UpdateList.Add(Item);
            return true;
        }

        /// <summary>
        /// Returs width of the maze in units
        /// </summary>
        /// <returns>Width of maze</returns>
        public int UnitWidth()
        {
            return unitWidth;
        }

        /// <summary>
        /// Returs height of the maze in units
        /// </summary>
        /// <returns>height of maze</returns>
        public int UnitHeight()
        {
            return unitHeight;
        }

        /// <summary>
        /// Returs width of the mazeblock unit
        /// </summary>
        /// <returns>width of mazeblock</returns>
        public int CellWidth()
        {
            return cellWidth;
        }

        /// <summary>
        /// Returs height of the mazeblock unit
        /// </summary>
        /// <returns>height of mazeblock</returns>
        public int CellHeight()
        {
            return cellHeight;
        }

        /// <summary>
        /// Returns full width of the entire maze
        /// </summary>
        /// <returns>Width of entire maze</returns>
        public int LevelWidth()
        {
            return levelWidth;
        }

        /// <summary>
        /// Returns full height of the entire maze
        /// </summary>
        /// <returns>Height of entire maze</returns>
        public int LevelHeight()
        {
            return levelHeight;
        }

        /// <summary>
        /// Increase counter for newly discovered Mazeblocks
        /// </summary>
        public void NewDiscovery()
        {
            comboTimer.Reset();
            comboTimer.Start();
            combo += 1;
            if (MaxCombo < combo) MaxCombo = combo;
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
            foreach (GameObject Item in UpdateList)
            {
                Item.Reset();
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
            
            foreach (GameObject Obj in DrawList)
            {
                Obj.Draw(batch);
            }

            float minutes = (int)timeLeft.CurrentTime() / 60;
            float seconds = (int)(timeLeft.CurrentTime() % 60);
            Utilities.Instance().DrawString(batch, "Time Left!  " + minutes + ":" + seconds, new Vector2(10, 10));
            Utilities.Instance().DrawString(batch, "Level:      " + LevelManager.Instance().CurrentLevel(), new Vector2(10, 30));
            Utilities.Instance().DrawString(batch, "MaxCombo:      " + MaxCombo, new Vector2(10, 50));
            if(combo > 1) Utilities.Instance().DrawString(batch, "Combo:      " + combo, new Vector2(10, 70));
        }

        public Vector2 NormalizedCoorindates(Vector2 Position) {
            return Position / new Vector2(levelWidth, levelHeight);
        }

        /// <summary>
        /// Collision detection for all obects against the Avatar.
        /// </summary>
        public void Collision()
        {
            for (int index = 0; index < UpdateList.Count; index++)
            {
                GameObject Item = UpdateList.ElementAt<GameObject>(index);
                GameObject Etem = Avatar.Instance();
                if (Vector2.Distance(Item.kinetics.position, Etem.kinetics.position) > 150)
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

        /// <summary>
        /// Collision Detection for two objects.
        /// </summary>
        public void Collision(GameObject One, GameObject Two)
        {
            if (Vector2.Distance(One.kinetics.position, Two.kinetics.position) < 150)
            {
                if (One.kinetics.boundingBox.Intersects(Two.kinetics.boundingBox))
                {
                    One.CollisionResolution(Two);
                    Two.CollisionResolution(One);
                }
            }
        }
    }
}

// Bamburner - As a People - Carpet Bomb Ballet