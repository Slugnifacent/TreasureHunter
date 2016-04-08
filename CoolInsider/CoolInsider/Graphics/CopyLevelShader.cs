using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    class CopyLevelShader
    {
        GraphicsDevice graphics;
        public CopyLevelShader(GraphicsDeviceManager Device)
        {
            graphics = Device.GraphicsDevice;
        }

        public void Update() {}

        public void Draw(SpriteBatch batch, ref RenderTarget2D renderTarget)
        {
            TreasureHunter.graphics.GraphicsDevice.SetRenderTarget(renderTarget);
            graphics.Clear(Color.SteelBlue);
            batch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, CameraManager.Instance().GetCamera().View());
            LevelManager.Instance().getCurrentLevel().Draw(batch);
            batch.End();
            TreasureHunter.graphics.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
