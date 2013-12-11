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
            int couter = 0;
            Chunk c;
            Vector2 v;
            const int o1 = Chunk.CHUNK_SIZE * 32;
            const int o2 = 16 * Chunk.CHUNK_SIZE;
            for (int x = 0; x < world.ChunksInRow; x++)
            {
                for (int y = 0; y < world.ChunksInRow; y++)
                {
                    c = world.GetChunk(x, y);
                    int mx = (world.ChunksInRow - y + x) * o1;
                    int my = (x + y) * o2;
                    v = new Vector2(mx, my);
                    //Text.Draw(v.X + " " + v.Y, v, Color.Azure, 0.4f);
                    GameMain.SpriteBatch.Draw(c.RenderTarget, v, Color.White);
                    couter++;
                    if(couter == 15)
                    {
                        GameMain.SpriteBatch.End();
                        GameMain.BeginNormalDrawing();
                    }
                }
            }

            //Vector2 v;
            //Chunk c;
            //// Dwie wartości stworzone w celach optymalizacyjnych
            //const int o1 = Chunk.CHUNK_SIZE * 32;
            //const int o2 = 16 * Chunk.CHUNK_SIZE;
            //int counter = 0;
            //for (int x = 0; x < world.ChunksInRow; x++)
            //{
            //    for (int y = 0; y < world.ChunksInRow; y++)
            //    {
            //        c = world.GetChunk(x, y);
            //        int mx = (world.ChunksInRow - y + x) * o1;
            //        int my = (x + y) * o2;
            //        for (ushort cx = 0; cx < Chunk.CHUNK_SIZE; cx++)
            //        {
            //            for (ushort cy = 0; cy < Chunk.CHUNK_SIZE; cy++)
            //            {
            //                // Wszystkie bloki są obracane w prawo o 45* aby stworzyć wrażenie izometrii
            //                v = new Vector2((Chunk.CHUNK_SIZE - cy + cx) * 32 + mx, (cx + cy) * 16 + my);

            //                //batch.Draw(Block.Blocks[chunk[cx, cy]].Texture, new Vector2((Chunk.CHUNK_SIZE - cy + cx) * 32 + ((chunk.WorldObj.ChunksInRow - chunk.Y + chunk.X) * Chunk.CHUNK_SIZE * 32), (cx + cy) * 16 + ((chunk.X + chunk.Y) * 16 * Chunk.CHUNK_SIZE)), Color.White);
            //                batch.Draw(Block.Blocks[c[cx, cy]].Texture, v, Color.White);
            //            }
            //        }
            //        if (counter % 15 == 0)
            //        {
            //            batch.End();
            //            GameMain.GameMain.Instance.BeginNormalDrawing();
            //        }
            //        counter++;
            //    }
            //}
        }
    }
}
