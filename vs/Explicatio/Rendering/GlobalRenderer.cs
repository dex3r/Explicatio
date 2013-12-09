using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Explicatio.Worlds;
using Explicatio.Blocks;

namespace Explicatio.Rendering
{
    public static class GlobalRenderer
    {
        private static RenderTarget2D renderTarget;

        public static void Draw(SpriteBatch batch, GameTime time, Camera camera)
        {
            //DrawWorld(batch, Main.Main.Instance.CurrentWorld, camera);
            renderTarget = new RenderTarget2D(Main.Main.Instance.GraphicsDevice, Chunk.CHUNK_SIZE * 64 + 64, Chunk.CHUNK_SIZE * 32);
            batch.End();
            batch.GraphicsDevice.SetRenderTarget(renderTarget);
            batch.Begin();
            batch.GraphicsDevice.Clear(Color.Transparent);
            World world = Main.Main.Instance.CurrentWorld;
            Vector2 v;
            Chunk c = c = world.GetChunk(0, 0);
            for (ushort cx = 0; cx < Chunk.CHUNK_SIZE; cx++)
            {
                for (ushort cy = 0; cy < Chunk.CHUNK_SIZE; cy++)
                {
                    // Wszystkie bloki są obracane w prawo o 45* aby stworzyć wrażenie izometrii
                    v = new Vector2((Chunk.CHUNK_SIZE - cy + cx) * 32, (cx + cy) * 16);

                    //batch.Draw(Block.Blocks[chunk[cx, cy]].Texture, new Vector2((Chunk.CHUNK_SIZE - cy + cx) * 32 + ((chunk.WorldObj.ChunksInRow - chunk.Y + chunk.X) * Chunk.CHUNK_SIZE * 32), (cx + cy) * 16 + ((chunk.X + chunk.Y) * 16 * Chunk.CHUNK_SIZE)), Color.White);
                    batch.Draw(Block.Blocks[c[cx, cy]].Texture, v, Color.White);
                }
            }
            batch.End();
            batch.GraphicsDevice.SetRenderTarget(null);
            batch.GraphicsDevice.Clear(Color.CornflowerBlue);
            Main.Main.Instance.BeginNormalDrawing();
            DrawWorld(batch, Main.Main.Instance.CurrentWorld, camera);
        }



        public static void DrawWorld(SpriteBatch batch, World world, Camera camera)
        {
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
                    batch.Draw(renderTarget, v, Color.White);
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
            //            Main.Main.Instance.BeginNormalDrawing();
            //        }
            //        counter++;
            //    }
            //}
        }
    }
}
