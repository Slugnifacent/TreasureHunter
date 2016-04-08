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
        public RenderTarget2D renderTarget;
        public RenderTarget2D renderTarget2;
        public RenderTarget2D blankTarget;
        int currentLevel = 1;
        float radiate;
        Effect radar;

        int levelTime;
        int levelWidth;
        int levelHeight;

        /// <summary>
        /// Constructor
        /// </summary>
        LevelManager()
        {
            renderTarget = new RenderTarget2D(
                TreasureHunter.graphics.GraphicsDevice,
                TreasureHunter.graphics.PreferredBackBufferWidth,
                TreasureHunter.graphics.PreferredBackBufferHeight);
            renderTarget2 = new RenderTarget2D(
                TreasureHunter.graphics.GraphicsDevice,
                TreasureHunter.graphics.PreferredBackBufferWidth,
                TreasureHunter.graphics.PreferredBackBufferHeight);
            blankTarget = new RenderTarget2D(
                TreasureHunter.graphics.GraphicsDevice,
                TreasureHunter.graphics.PreferredBackBufferWidth,
                TreasureHunter.graphics.PreferredBackBufferHeight);
            radar = TreasureHunter.content.Load<Effect>("Radar");
            SamplerState state = new SamplerState();
            state.AddressU = TextureAddressMode.Clamp;
            state.AddressV = TextureAddressMode.Clamp;
            state.Filter = TextureFilter.LinearMipPoint;
            
            state.MaxMipLevel = 5;
            TreasureHunter.graphics.GraphicsDevice.SamplerStates[0] = state;
            TreasureHunter.graphics.GraphicsDevice.SamplerStates[1] = state;
            radiate = 0;
            levelTime = 60;
            levelWidth = levelHeight = 25;
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
        }

        /// <summary>
        /// Draws the Current level to the screen, including radar.
        /// </summary>
        /// <param name="batch">Microsoft Sprite Batch</param>
        public void Draw(SpriteBatch batch)
        {
           
            batch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, TreasureHunter.Cam.View());
            current.Draw(batch);
            batch.End();
            TreasureHunter.graphics.GraphicsDevice.SetRenderTarget(null);


            TreasureHunter.graphics.GraphicsDevice.SetRenderTarget(renderTarget2);
            TreasureHunter.graphics.GraphicsDevice.Clear(Color.Transparent);

            radar.CurrentTechnique = radar.Techniques["Technique1"];
            TreasureHunter.graphics.GraphicsDevice.Textures[0] = blankTarget;
            TreasureHunter.graphics.GraphicsDevice.Textures[1] = current.radar.map;

            Vector2 temp = Vector2.Zero;
            temp.X = Avatar.Instance().kinetics.boundingBox.X + Avatar.Instance().kinetics.boundingBox.Width / 2;
            temp.Y = Avatar.Instance().kinetics.boundingBox.Y + Avatar.Instance().kinetics.boundingBox.Height / 2;
            temp = temp / new Vector2(current.ScaledWidth(), current.ScaledHeight());

            Vector2 temp2 = Vector2.Zero;
            temp2.X = current.treasure.kinetics.position.X;
            temp2.Y = current.treasure.kinetics.position.Y;
            temp2 = temp2 / new Vector2(current.ScaledWidth(), current.ScaledHeight());

            radar.Parameters["AvatarPosition"].SetValue(temp);
            radar.Parameters["TreasurePosition"].SetValue(temp2);
            float tempradiate = (float)Math.Sin(radiate);
            switch (current.treasure.color)
            {
                case 0:
                    radar.Parameters["GemColor"].SetValue(new Vector4(0, 0, 1, tempradiate));
                    break;
                case 1:
                    radar.Parameters["GemColor"].SetValue(new Vector4(0, 1, 0, tempradiate));
                    break;
                case 2:
                    radar.Parameters["GemColor"].SetValue(new Vector4(1, .657f, 0, tempradiate));
                    break;
             }
            batch.Begin(SpriteSortMode.Immediate, null, null, null, null, radar);
            batch.Draw(blankTarget, Vector2.Zero, Color.White);
            batch.End();
            radiate += .1f;

            TreasureHunter.graphics.GraphicsDevice.SetRenderTarget(null);

            batch.Begin();
            batch.Draw(renderTarget, Vector2.Zero, Color.White);
            batch.End();

            batch.Begin();
            batch.Draw(renderTarget2,
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
            ScreenManager.Instance().Push(new ScoreScreen((int)current.timeLeft.CurrentTime(), current.death, renderTarget));
            levelWidth++;
            levelHeight++;
            current = new Level(levelWidth, levelHeight, current.ScaleX(), current.ScaleY(), levelTime);
            Avatar.Instance().Revive();
            current.InsertGameObject(Avatar.Instance());
            Avatar.Instance().kinetics.position = current.firstPosition;
            currentLevel++;
        }

        /// <summary>
        /// Generates the first level
        /// </summary>
        public void GenerateFirstLevel()
        {
            current = new Level(levelWidth, levelHeight, 101, 82, levelTime);
            Avatar.Instance().Revive();
            current.InsertGameObject(Avatar.Instance());
            Avatar.Instance().kinetics.position = current.firstPosition;
            currentLevel = 1;
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
            ScreenManager.Instance().Push(new GameOverScreen(renderTarget));
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
