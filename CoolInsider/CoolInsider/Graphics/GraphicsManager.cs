using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreasureHunter
{
    class GraphicsManager
    {
        readonly public static RenderTarget2D BlankTarget = new RenderTarget2D(TreasureHunter.graphics.GraphicsDevice,
                    TreasureHunter.graphics.PreferredBackBufferWidth,
                    TreasureHunter.graphics.PreferredBackBufferHeight);

        public static RenderTarget2D renderTargetScene = new RenderTarget2D(TreasureHunter.graphics.GraphicsDevice,
                    TreasureHunter.graphics.PreferredBackBufferWidth,
                    TreasureHunter.graphics.PreferredBackBufferHeight);

        public static RenderTarget2D renderTargetRadar = new RenderTarget2D(TreasureHunter.graphics.GraphicsDevice,
                    TreasureHunter.graphics.PreferredBackBufferWidth,
                    TreasureHunter.graphics.PreferredBackBufferHeight);

        public static RenderTarget2D renderTargetLight = new RenderTarget2D(TreasureHunter.graphics.GraphicsDevice,
                    TreasureHunter.graphics.PreferredBackBufferWidth,
                    TreasureHunter.graphics.PreferredBackBufferHeight);

        public static RenderTarget2D renderTargetShadows = new RenderTarget2D(TreasureHunter.graphics.GraphicsDevice,
                    TreasureHunter.graphics.PreferredBackBufferWidth,
                    TreasureHunter.graphics.PreferredBackBufferHeight);

        public static RenderTarget2D renderTargetFinal = new RenderTarget2D(TreasureHunter.graphics.GraphicsDevice,
                    TreasureHunter.graphics.PreferredBackBufferWidth,
                    TreasureHunter.graphics.PreferredBackBufferHeight);

        public static void SetSamplerState(int Index, SamplerState Sampler) {
            TreasureHunter.graphics.GraphicsDevice.SamplerStates[Index] = Sampler;
        }

        public static void CLEAR() { 
        
        
        }
    }

}
