using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Explicatio.Worlds;

namespace Explicatio.Rendering
{
    public static class GlobalRenderer
    {
        public static void Draw(SpriteBatch batch, GameTime time)
        {
            DrawWorld(batch, Main.Main.Instance.CurrentWorld);
        }

        public static void DrawWorld(SpriteBatch batch, World world)
        {
            batch.End();
            for(int x = 0; x < world.ChunksInRow; x++)
            {
                for(int y = 0; y < world.ChunksInRow; y++)
                {
                    // Przesunięcie stanu SpriteBatch do pozycji, od której ma być rysowany nowy chyunk
                    Main.Main.Instance.BeginDrawingAndApplyTransformation(Matrix.CreateTranslation((world.ChunksInRow - y + x) * Chunk.CHUNK_SIZE * 32, (x + y) * 16 * Chunk.CHUNK_SIZE, 0));
                    ChunkRenderer.DrawChunk(batch, world.GetChunk(x, y));
                    batch.End();
                }
            }
            // Przywrócenie pozycji kamery
           Main.Main.Instance.BeginNormalDrawing();
        }
    }
}
