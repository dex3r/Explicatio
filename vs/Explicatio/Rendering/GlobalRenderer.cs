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
using Explicatio.Rendering;
using Explicatio.Utils;

namespace Explicatio.Rendering
{
    public static class GlobalRenderer
    {
        public static void Draw(GameTime time)
        {
            GameMain.SpriteBatch.End();
            World world = Main.GameMain.CurrentWorld;
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
            DrawWorld(Main.GameMain.CurrentWorld);
        }

        public static void DrawWorld(World world)
        {
            GameMain.LastDrawedChunksCount = 0;
            Chunk c;
            Vector2 v;
            // const dla wydajności
            const int o1 = Chunk.CHUNK_SIZE * 32;
            const int o2 = Chunk.CHUNK_SIZE * 16;
            // Pozycja chunka do wyrenderowania
            int mx, my;
            // Pozycja pierwszego widocznego pixela
            float startscreenX = -Camera.Transform.Translation.X * (1 / Camera.Zoom);
            float startscreenY = -Camera.Transform.Translation.Y * (1 / Camera.Zoom);
            // Pozycja ostatniego widocznego pixela
            float endscreenX = startscreenX + Main.GameMain.SpriteBatch.GraphicsDevice.Viewport.Width * (1 / Camera.Zoom);
            float endscreenY = startscreenY + Main.GameMain.SpriteBatch.GraphicsDevice.Viewport.Height * (1 / Camera.Zoom);
            for (int x = 0; x < world.ChunksInRow; x++)
            {
                for (int y = 0; y < world.ChunksInRow; y++)
                {
                    c = world.GetChunk(x, y);
                    mx = (world.ChunksInRow - y + x) * o1;
                    my = (x + y) * o2;
                    // Rysowanie tylko widocznych chunków
                    if (mx + c.RenderTarget.Width < startscreenX - c.RenderTarget.Height || my + c.RenderTarget.Height < startscreenY || mx > endscreenX || my > endscreenY)
                    {
                        continue;
                    }
                    //Pozycja chunka do narysowania
                    v = new Vector2(mx, my);
                    GameMain.SpriteBatch.Draw(c.RenderTarget, v, Color.White);
                    GameMain.LastDrawedChunksCount++;
                }
            }
        }
    }
}
