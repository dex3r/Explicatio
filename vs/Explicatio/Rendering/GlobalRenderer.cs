using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Explicatio.Worlds;
using Explicatio.Blocks;
using Explicatio.Main;

namespace Explicatio.Rendering
{
    public static class GlobalRenderer
    {
        public static void Draw(GameTime time)
        {
            GameMain.SpriteBatch.End();
            World world = Main.GameMain.Instance.CurrentWorld;
            Chunk c;
            for (int x = 0; x < world.ChunksInRow; x++)
            {
                for (int y = 0; y < world.ChunksInRow; y++)
                {
                    c = world.GetChunk(x, y);
                    if(c.NeedsRedrawing)
                    {
                        ChunkRenderer.RenderChunk(c);
                    }
                }
            }
            GameMain.BeginNormalDrawing();
            GameMain.SpriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawWorld(Main.GameMain.Instance.CurrentWorld);
        }

        public static void DrawWorld(World world)
        {
            Chunk c;
            Vector2 v;
            // const dla wydajności
            const int o1 = Chunk.CHUNK_SIZE * 32;
            const int o2 = Chunk.CHUNK_SIZE * 16;
            // pozycja chunka do wyrenderowania
            int mx, my;
            float relativeX = -Rendering.Camera.Transform.Translation.X * (1 / Rendering.Camera.Zoom);
            float relativeY = -Rendering.Camera.Transform.Translation.Y * (1 / Rendering.Camera.Zoom);
            for (int x = 0; x < world.ChunksInRow; x++)
            {
                for (int y = 0; y < world.ChunksInRow; y++)
                {
                    c = world.GetChunk(x, y);
                    mx = (world.ChunksInRow - y + x) * o1;
                    my = (x + y) * o2;
                    v = new Vector2(mx, my);
                    //Text.Draw(v.X + " " + v.Y, v, Color.Azure, 0.4f);
                    GameMain.SpriteBatch.Draw(c.RenderTarget, v, Color.White);
                }
            }
        }
    }
}
