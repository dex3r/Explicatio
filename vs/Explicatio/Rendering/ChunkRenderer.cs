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
            Vector2 v;
            for (ushort x = 0; x < Chunk.CHUNK_SIZE; x++)
            {
                for (ushort y = 0; y < Chunk.CHUNK_SIZE; y++)
                {
                    // Wszystkie bloki są obracane w prawo o 45* aby stworzyć wrażenie izometrii
                    v = new Vector2((Chunk.CHUNK_SIZE - y + x) * 32, (x + y) * 16);
                    
                    //batch.Draw(Block.Blocks[chunk[x, y]].Texture, new Vector2((Chunk.CHUNK_SIZE - y + x) * 32 + ((chunk.WorldObj.ChunksInRow - chunk.Y + chunk.X) * Chunk.CHUNK_SIZE * 32), (x + y) * 16 + ((chunk.X + chunk.Y) * 16 * Chunk.CHUNK_SIZE)), Color.White);
                    batch.Draw(Block.Blocks[chunk[x, y]].Texture, v, Color.White);
                }
            }
            //TODO: dodać renderowanie pojazdów i budynków
        }
    }
}
