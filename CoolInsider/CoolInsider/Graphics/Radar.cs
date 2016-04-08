using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    public class Radar
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Class that represents the Radar of the game
         * Note: This class is used in combination with 
         * shader effects in LevelManager.Draw()
         */
        public Texture2D map;

        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="Maze">Maze that will be used for referencing</param>
        public Radar(MazeBlock[,] Maze)
        {
            map = new Texture2D(
                TreasureHunter.graphics.GraphicsDevice,
                Maze.GetLength(0),
                Maze.GetLength(1), false, SurfaceFormat.Color);
            Color[] data = new Color[Maze.Length];
            for (int index = 0; index < Maze.GetLength(0); index++)
            {
                for (int endex = 0; endex < Maze.GetLength(1); endex++)
                {
                    MazeBlock temp = Maze[index,endex];
                    if (temp != null)
                    {
                        if (temp.Walkable)
                        {
                            data[index + endex * Maze.GetLength(0)] = Color.Red;
                            continue;
                        }
                    }
                    data[index + endex * Maze.GetLength(0)] = Color.Transparent;
                }
            }
            map.SetData(data);
        }
    }
}
