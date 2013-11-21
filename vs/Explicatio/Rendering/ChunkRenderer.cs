using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Explicatio.Worlds;
using Explicatio.Blocks;

namespace Explicatio.Rendering
{
    public static class ChunkRenderer
    {
        public static void DrawChunk(SpriteBatch batch, Chunk chunk)
        {
            for (ushort cx = 0; cx < Chunk.CHUNK_SIZE; cx++)
            {
                for (ushort cy = 0; cy < Chunk.CHUNK_SIZE; cy++)
                {
                    // Wszystkie bloki są obracane w prawo o 45* aby stworzyć wrażenie izometrii
                    //v = new Vector2((Chunk.CHUNK_SIZE - cy + cx) * 32, (cx + cy) * 16);

                    //batch.Draw(Block.Blocks[chunk[cx, cy]].Texture, new Vector2((Chunk.CHUNK_SIZE - cy + cx) * 32 + ((chunk.WorldObj.ChunksInRow - chunk.Y + chunk.X) * Chunk.CHUNK_SIZE * 32), (cx + cy) * 16 + ((chunk.X + chunk.Y) * 16 * Chunk.CHUNK_SIZE)), Color.White);
                    //batch.Draw(Block.Blocks[chunk[cx, cy]].Texture, v, Color.White);
                }
            }
            //TODO: dodać renderowanie pojazdów i budynków
        }
    }
}
