using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    class Light
    {
        GraphicsDevice graphics;
        Effect shader;
        Effect shaderShadows;
        public Color color;
        public Vector2 position;
        public Vector2 camPosition;
        public bool off;
        public Light(GraphicsDeviceManager Device)
        {
            graphics = Device.GraphicsDevice;
            shader = TreasureHunter.content.Load<Effect>("LightSource");
            shaderShadows = TreasureHunter.content.Load<Effect>("AdditiveBlend");
            color = Color.Green;
            position = new Vector2(100, 100);
            
        }

        public void Update() {}

        public void Draw(SpriteBatch batch,ref RenderTarget2D Scene)
        {
            if (Vector2.Distance(position, Avatar.Instance().kinetics.position) > 1000)
            {
                off = true;
                return;
            }
            else off = false;
            
            // RendersScene with Lights
            TreasureHunter.graphics.GraphicsDevice.SetRenderTarget(GraphicsManager.renderTargetLight);
            graphics.Clear(Color.Transparent);
            camPosition = CameraManager.Instance().GetCamera().NormalizeCoordinates(position);
            camPosition *= -1;
            shader.Parameters["lightSource"].SetValue(camPosition);
            shader.Parameters["sourceLightReach"].SetValue((float)(Math.Sin(RadarShader.radiate)/2 + .7f));
            shader.Parameters["sourceLightColor"].SetValue(color.ToVector4());
            batch.Begin(SpriteSortMode.Immediate, null, null, null, null, shader);
            batch.Draw(GraphicsManager.BlankTarget, Vector2.Zero, Color.White);
            batch.End();
            TreasureHunter.graphics.GraphicsDevice.SetRenderTarget(null);

            TreasureHunter.graphics.GraphicsDevice.SetRenderTarget(GraphicsManager.renderTargetFinal);
            graphics.Clear(Color.Transparent);
            graphics.Textures[1] = Scene;
            graphics.Textures[2] = GraphicsManager.renderTargetLight;

            batch.Begin(SpriteSortMode.Immediate, null, null, null, null, shaderShadows);
            batch.Draw(GraphicsManager.BlankTarget, Vector2.Zero, Color.White);
            batch.End();
            graphics.Textures[1] = null;
            graphics.Textures[2] = null;
            TreasureHunter.graphics.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
