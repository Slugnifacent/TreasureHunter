using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TreasureHunter
{
    public class LevelManager
    {

        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Provides a static interface to current level in the game.
         * 
         */

        static LevelManager manager;
        Level current;
        int currentLevel = 1;
        Effect radar;

        int levelTime;
        int levelWidth;
        int levelHeight;

        RadarShader radarShader;
        CopyLevelShader copyLevel;
        Light light;
        /// <summary>
        /// Constructor
        /// </summary>
        LevelManager()
        {
            radar = TreasureHunter.content.Load<Effect>("Radar");
            
            SamplerState state = new SamplerState();
            state.AddressU = TextureAddressMode.Clamp;
            state.AddressV = TextureAddressMode.Clamp;
            state.Filter = TextureFilter.LinearMipPoint;
            state.MaxMipLevel = 2;

            GraphicsManager.SetSamplerState(0, state);
            GraphicsManager.SetSamplerState(1, state);
            GraphicsManager.SetSamplerState(2, state);
            
            copyLevel = new CopyLevelShader(TreasureHunter.graphics);
        }

        /// <summary>
        /// Returns the singleton instance of this object
        /// </summary>
        /// <returns>Returns the static LevelManager instance</returns>
        public static LevelManager Instance()
        {
            if (manager == null)
            {
                manager = new LevelManager();
            }
            return manager;
        }

        /// <summary>
        /// Updates the current level
        /// </summary>
        public void Update()
        {
            current.Update();
            if(radarShader != null){
                radarShader.Update();
            }
        }

        /// <summary>
        /// Draws the Current level to the screen, including radar.
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        public void Draw(SpriteBatch batch)
        {
            copyLevel.Draw(batch, ref GraphicsManager.renderTargetScene);

            Vector2 temp = Vector2.Zero;
            temp.X = Avatar.Instance().kinetics.boundingBox.X + Avatar.Instance().kinetics.boundingBox.Width / 2;
            temp.Y = Avatar.Instance().kinetics.boundingBox.Y + Avatar.Instance().kinetics.boundingBox.Height / 2;

            radarShader.Draw(batch, ref GraphicsManager.renderTargetRadar, temp, current.treasure.kinetics.position);
            light.Draw(batch, ref GraphicsManager.renderTargetScene);

            batch.Begin();
            if (light.off)
            {
                batch.Draw(GraphicsManager.renderTargetScene, Vector2.Zero, Color.White);
            }
            else batch.Draw(GraphicsManager.renderTargetFinal, Vector2.Zero, Color.White);

            batch.Draw(GraphicsManager.renderTargetRadar,
                new Rectangle(TreasureHunter.graphics.PreferredBackBufferWidth - 200,
                    TreasureHunter.graphics.PreferredBackBufferHeight - 200, 200, 200), Color.White);
            batch.End();

        }

        /// <summary>
        /// Returns the current level
        /// </summary>
        /// <returns>Current Level</returns>
        public Level getCurrentLevel()
        {
            return current;
        }

        /// <summary>
        /// Sets the current level to the given level
        /// </summary>
        /// <param name="lvl">Level to set the current level to</param>
        public void SetLevel(Level lvl)
        {
            current = lvl;

        }

        /// <summary>
        /// Generates the next consecutive level
        /// </summary>
        public void GenerateNextLevel()
        {
            ScreenManager.Instance().Push(new ScoreScreen((int)current.timeLeft.CurrentTime(), current.MaxCombo,(int)current.discovered, GraphicsManager.renderTargetFinal));
            levelWidth++;
            levelHeight++;
            levelTime = levelHeight*3;
            current = new Level(levelWidth, levelHeight, current.CellWidth(), current.CellHeight(), levelTime);
            Avatar.Instance().Revive();
            current.InsertGameObject(Avatar.Instance());
            Avatar.Instance().kinetics.position = current.firstPosition;
            Avatar.Instance().Reset();
            currentLevel++;
            radarShader = new RadarShader(TreasureHunter.graphics, current.radar.map, radar);
            light = new Light(TreasureHunter.graphics);
            light.position.X = current.treasure.kinetics.position.X + current.treasure.kinetics.boundingBox.Width/2;
            light.position.Y = current.treasure.kinetics.position.Y + current.treasure.kinetics.boundingBox.Height/2;
            light.color = current.treasure.color;
       }

        /// <summary>
        /// Generates the first level
        /// </summary>
        public void GenerateFirstLevel()
        {
            levelWidth = levelHeight = 8;
            levelTime = levelHeight * 3;
            current = new Level(levelWidth, levelHeight, 101, 82, levelTime);
            Avatar.Instance().Revive();
            current.InsertGameObject(Avatar.Instance());
            Avatar.Instance().kinetics.position = current.firstPosition;
            currentLevel = 15;
            radarShader = new RadarShader(TreasureHunter.graphics, current.radar.map,radar);
            light = new Light(TreasureHunter.graphics);
            light.position.X = current.treasure.kinetics.position.X + current.treasure.kinetics.boundingBox.Width / 2;
            light.position.Y = current.treasure.kinetics.position.Y + current.treasure.kinetics.boundingBox.Height / 2;
            light.color = current.treasure.color;
        }

        /// <summary>
        /// Restarts the level to its standard configurations
        /// </summary>
        public void RestartLevel()
        {
            Avatar.Instance().Revive();
            current.Reset();
            Avatar.Instance().kinetics.position = current.firstPosition;
        }

        /// <summary>
        /// Pushes the GameOverScreen onto the screen stack
        /// </summary>
        public void GameOver() {
            ScreenManager.Instance().Push(new GameOverScreen(GraphicsManager.renderTargetScene));
        }

        /// <summary>
        /// Returns the current level number
        /// </summary>
        /// <returns>Current Level</returns>
        public int CurrentLevel()
        {
            return currentLevel;
        }

        /// <summary>
        /// Notifies the Level of newly discovered terrain
        /// </summary>
        public void Discovered() {
            SoundManager.Instance().AddSound(new SFX("SoundEffects\\ButtonClick"));
            current.NewDiscovery();
        }
    }
}
