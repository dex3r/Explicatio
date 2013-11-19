﻿using System;
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
        //? TEMP!!!
        public static Chunk c;
        public static void Draw(SpriteBatch batch, GameTime time, Chunk chunk)
        {

            for (ushort x = 0; x < Chunk.CHUNK_SIZE; x++)
            {
                for (ushort y = 0; y < Chunk.CHUNK_SIZE; y++)
                {
                    // Wszystkie bloki są obracane w prawo 0 45* aby stworzyć wrażenie izometrii
                    //!? Poprawić, jak chcesz to naprawiaj
                    batch.Draw(Block.Blocks[chunk[x, y]].Texture, new Vector2((Chunk.CHUNK_SIZE - y + x) * 32, (Chunk.CHUNK_SIZE + x - y) * 32), Color.White);
                }
            }
            //TODO: dodać renderowanie pojazdów x budynków
        }
    }
}