﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TreasureHunter
{
    public class MazeGenerator
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Algorithm that generates the Mazes found in the game.
         */

        public MazeBlock[,] maze;
        int width;
        int height;
        int unitWidth;
        int unitHeight;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Width">Width of the maze in units</param>
        /// <param name="Height">Height of the maze in units</param>
        /// <param name="ScaleX">Width of a maze unit</param>
        /// <param name="ScaleY">Height of a maze unit</param>
        public MazeGenerator(int Width, int Height,int UnitWidth, int UnitHeight) {
           
            width = Width;
            height = Height;
            unitWidth = UnitWidth;
            unitHeight = UnitHeight;
            maze = new MazeBlock[Width, Height];
            for (int index = 0; index < width; index++)
            {
                for (int endex = 0; endex < height; endex++)
                {
                    maze[index, endex] = new MazeBlock();
                    maze[index, endex].Walkable = false;
                    maze[index, endex].kinetics.position = new Vector2(index * unitWidth, endex * unitHeight);
                    maze[index, endex].kinetics.maxSpeed = 0;
                    maze[index, endex].kinetics.Update();
                }
            }
            
            GenerateMaze();
            AssignNeighbors();
            PruneBlocks();
        }

        /// <summary>
        /// Prunes unneccessary blocks that have been Created.
        /// </summary>
        void PruneBlocks() {
            for (int index = 0; index < width; index++)
            {
                for (int endex = 0; endex < height; endex++)
                {
                    if (!IndexWalkable(index, endex - 1))
                    {
                        if (!IndexWalkable(index, endex + 1))
                        {
                            if (!IndexWalkable(index - 1, endex))
                            {
                                if (!IndexWalkable(index + 1, endex))
                                {
                                    if (!IndexWalkable(index + 1, endex + 1))
                                    {
                                        if (!IndexWalkable(index - 1, endex + 1))
                                        {
                                            if (!IndexWalkable(index + 1, endex - 1))
                                            {
                                                if (!IndexWalkable(index - 1, endex - 1))
                                                {
                                                    maze[index, endex] = null;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        bool IndexWalkable(int X, int Y) {
            if (inBounds(X, 0, width) && inBounds(Y, 0, height))
            {
                if (maze[X, Y] != null)
                {
                    if (maze[X, Y ].Walkable)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Assigns the neighbors of each block.
        /// </summary>
        void AssignNeighbors()
        {
            for (int index = 0; index < width; index++)
            {
                for (int endex = 0; endex < height; endex++)
                {
                    if (inBounds(index - 1, 0, width))
                    {
                        maze[index, endex].Left = maze[index - 1, endex];
                    }
                    if (inBounds(index + 1, 0, width))
                    {
                        maze[index, endex].Right = maze[index + 1, endex];
                    }

                    if (inBounds(endex - 1, 0, height))
                    {
                        maze[index, endex].Up = maze[index  , endex - 1];
                    }

                    if (inBounds(endex + 1, 0, height))
                    {
                        maze[index, endex].Down = maze[index, endex + 1];
                    }
                }
            }
        }

        /// <summary>
        /// Checks if value is within the range(min and max
        /// </summary>
        /// <param name="Value">Value to check</param>
        /// <param name="Min">Minimum Value</param>
        /// <param name="Max">Maximum Value</param>
        /// <returns>Returns true if the Value is with the range(min,Max)</returns>
        bool inBounds(int Value, int Min, int Max) {
            if (Value >= Max) return false;
            if (Value < 0) return false;
            return true;
        }

        /// <summary>
        /// Starter Maze Generation
        /// </summary>
        void GenerateMaze() {
            // x = 0; y = 1;
            int x = Utilities.Instance().rand.Next(1, maze.GetLength(0) - 1);
            int y = Utilities.Instance().rand.Next(1, maze.GetLength(1) - 1);
            maze[x, y].Walkable = true;
            DigMaze(x, y,0);
        }

        /// <summary>
        /// Recoursive maze generation algorithm
        /// </summary>
        /// <param name="X">X component of the algorithms position in the maze</param>
        /// <param name="Y">Y component of the algorithms position in the maze</param>
        void DigMaze(int X, int Y, int RecursionNumber)
        {
            //if (RecursionNumber > width/2) return;
            int[] directions = Utilities.Instance().GenerateRandomNumberList(0, 4);
            int bound = 2;
            for (int index = 0; index < directions.Count(); index++) {
                switch (directions[index]) { 
                    case 0: // Up
                        if (Utilities.Instance().InBounds(Y - 2, 1 + bound, height - bound))
                        {
                            if (!maze[X, Y - 2].Walkable) {
                                 maze[X, Y - 1].Walkable = true;
                                 maze[X, Y - 2].Walkable = true;
                                 DigMaze(X, Y - 2, ++RecursionNumber);
                            }
                        }
                        break;
                    case 1: // Down
                        if (Utilities.Instance().InBounds(Y + 2, 1 + bound, height - bound))
                        {

                            if (!maze[X, Y + 2].Walkable)
                            {
                                maze[X, Y + 1].Walkable = true;
                                maze[X, Y + 2].Walkable = true;
                                DigMaze(X, Y + 2, ++RecursionNumber);
                            }
                        }
                        break;
                    case 2: // Left
                        if (Utilities.Instance().InBounds(X - 2, 1 + bound, width - bound))
                        {

                            if (!maze[X - 2, Y].Walkable)
                            {
                                maze[X - 1 , Y].Walkable = true;
                                maze[X - 2 , Y].Walkable = true;
                                DigMaze(X - 2, Y, ++RecursionNumber);
                            }
                        }
                        break;
                    case 3: // Right
                        if (Utilities.Instance().InBounds(X + 2, 1 + bound, width - bound))
                        {

                            if (!maze[X + 2, Y].Walkable)
                            {
                                maze[X + 1, Y].Walkable = true;
                                maze[X + 2, Y].Walkable = true;
                                DigMaze(X + 2, Y, ++RecursionNumber);
                            }
                        }
                        break;
                }
                
            }

        }



        /// <summary>
        /// Returns the width of the maze Scaled 
        /// </summary>
        /// <returns>Width of maze scaled</returns>
        public int MazeWidth()
        {
            return maze.GetLength(0) * unitWidth;
        }

        /// <summary>
        /// Returns the height of the maze Scaled 
        /// </summary>
        /// <returns>Height of maze scaled</returns>
        public int MazeHeight()
        {
            return maze.GetLength(1) * unitHeight;
        }

        /// <summary>
        /// Write the Maze to a string
        /// </summary>
        /// <returns>String representation of the maze</returns>
        public override string ToString(){
            string result = "";
            for (int index = 0; index < width; index++)
            {
                for (int endex = 0; endex < height; endex++)
                {
                    if(maze[index,endex].Walkable){
                        result += '0';
                    }
                    else result += '-';
                }
                result += '\n';
            }
            return result;
        }
    }
}