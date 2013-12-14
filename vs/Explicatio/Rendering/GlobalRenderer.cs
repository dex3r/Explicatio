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
            GameMain.Instance.LastDrawedChunksCount = 0;
            Chunk c;
            Vector2 v;
            // const dla wydajności
            const int o1 = Chunk.CHUNK_SIZE * 32;
            const int o2 = Chunk.CHUNK_SIZE * 16;
            // pozycja chunka do wyrenderowania
            int mx, my;
            float relativeX = Camera.Transform.Translation.X * (1 / Camera.Zoom);
            float relativeY = Camera.Transform.Translation.Y * (1 / Camera.Zoom);
            float startY = ((relativeX / o1) - (relativeY / o2) - world.ChunksInRow) / -2;
            float startX = (relativeY / o2) - startY;
            for (int x = (int)Math.Max(0, Math.Floor(startX)); x < world.ChunksInRow; x++)
            {
                for (int y = (int)Math.Max(0, Math.Floor(startY)); y < world.ChunksInRow; y++)
                {
                    c = world.GetChunk(x, y);
                    mx = (world.ChunksInRow - y + x) * o1;
                    my = (x + y) * o2;
                    v = new Vector2(mx, my);
                    //Text.Draw(v.X + " " + v.Y, v, Color.Azure, 0.4f);
                    GameMain.SpriteBatch.Draw(c.RenderTarget, v, Color.White);
                    GameMain.Instance.LastDrawedChunksCount++;
                }
            }

            GameMain.SpriteBatch.End();
            GameMain.BeginNormalDrawing();

            //!? Temp code
            for (int x = 0; x < world.ChunksInRow; x++)
            {
                for (int y = 0; y < world.ChunksInRow; y++)
                {
                    mx = (world.ChunksInRow - y + x) * o1;
                    my = (x + y) * o2;
                    v = new Vector2(mx, my);
                    Primitives2D.DrawRectangle(GameMain.SpriteBatch, new Rectangle(mx, my, Chunk.CHUNK_SIZE * 64 + 64, Chunk.CHUNK_SIZE * 32 + 16), Color.Red, 5f);
                }
            }
        }
    }
}
