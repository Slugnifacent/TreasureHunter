using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    class RadarShader
    {
        GraphicsDevice graphics;
        Effect radarShader;
        Texture2D radarMap;
        public static float radiate;
        public RadarShader(GraphicsDeviceManager Device, Texture2D RadarMap, Effect RadarShader)
        {
            graphics = Device.GraphicsDevice;
            radarMap = RadarMap;
            radarShader = RadarShader;
        }

        public void Update() {
            radiate += .05f;
        }

        public void Draw(SpriteBatch batch, ref RenderTarget2D renderTarget, Vector2 ObjectPositionONE, Vector2 ObjectPositionTWO)
        {
            graphics.SetRenderTarget(renderTarget);
            graphics.Clear(Color.Transparent);

            radarShader.CurrentTechnique = radarShader.Techniques["Technique1"];

            graphics.Textures[0] = GraphicsManager.BlankTarget;
            graphics.Textures[1] = radarMap;

            ObjectPositionONE = LevelManager.Instance().getCurrentLevel().NormalizedCoorindates(ObjectPositionONE);
            ObjectPositionTWO = LevelManager.Instance().getCurrentLevel().NormalizedCoorindates(ObjectPositionTWO);

            radarShader.Parameters["AvatarPosition"].SetValue(ObjectPositionONE);
            radarShader.Parameters["TreasurePosition"].SetValue(ObjectPositionTWO);
            radarShader.Parameters["GemColor"].SetValue(new Vector4(LevelManager.Instance().getCurrentLevel().treasure.color.ToVector3(), (float)Math.Sin(radiate)));

            batch.Begin(SpriteSortMode.Immediate, null, null, null, null, radarShader);
            batch.Draw(GraphicsManager.BlankTarget, Vector2.Zero, Color.White);
            batch.End();
            graphics.SetRenderTarget(null);
            graphics.Textures[0] = null;
            graphics.Textures[1] = null;
        }
    }
}
