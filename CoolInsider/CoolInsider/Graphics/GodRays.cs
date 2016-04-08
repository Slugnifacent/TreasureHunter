using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    class GodRays
    {
        GraphicsDevice graphics;
        Effect godRays;
        public GodRays(GraphicsDeviceManager Device)
        {
            graphics = Device.GraphicsDevice;
            godRays = TreasureHunter.content.Load<Effect>("GodRays");
        }

        public void Update() {}

        public void Draw(SpriteBatch batch, ref RenderTarget2D ObjectScene,ref RenderTarget2D RenderTarget, Vector2 position)
        {
            TreasureHunter.graphics.GraphicsDevice.SetRenderTarget(RenderTarget);
            graphics.Clear(Color.Transparent);
            godRays.Parameters["lightSource"].SetValue(new Vector2(.5f, .5f));
            godRays.Parameters["sourceLightReach"].SetValue(.8f);
            godRays.Parameters["sourceLightColor"].SetValue(Color.Green.ToVector4());

            godRays.Parameters["exposure"].SetValue(1);
            godRays.Parameters["density"].SetValue(1f);
            godRays.Parameters["weight"].SetValue(1f);
            godRays.Parameters["weight"].SetValue(.4f);

            batch.Begin(SpriteSortMode.BackToFront,null,null,null,null,godRays);
            batch.Draw(ObjectScene, Vector2.Zero, Color.White);
            batch.End();
            graphics.Textures[0] = null;
            graphics.Textures[1] = null;
            TreasureHunter.graphics.GraphicsDevice.SetRenderTarget(null);
            
        }
    }
}