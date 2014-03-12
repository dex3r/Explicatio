using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Worlds
{
    public class World
    {
        public const int WORLD_SIZE = 8;


        private Chunk[] chunks = new Chunk[WORLD_SIZE * WORLD_SIZE];
        private float chunkSizeWidth = (Chunk.CHUNK_SIZE * Block.BLOCK_WIDTH);
        private float chunkSizeHeight = (Chunk.CHUNK_SIZE * Block.BLOCK_HEIGHT);

        public World(bool defaultdata)
        {
            if (defaultdata)
            {
                for (int xi = 0; xi < WORLD_SIZE; xi++)
                {
                    for (int yi = 0; yi < WORLD_SIZE; yi++)
                    {
                        this[xi, yi] = new Chunk(true);
                    }
                }
            }
            else throw new NotImplementedException("Here should be world loading");
        }
        /// <summary>
        /// Zwraca chunk na świecie x,y to pozycja w tablicy chunka.
        /// </summary>
        /// <param name="x">Pozycja w tablicy</param>
        /// <param name="y">Pozycja w tablicy</param>
        public Chunk this[int x, int y]
        {
            get { return chunks[y * WORLD_SIZE + x]; }
            set { chunks[y * WORLD_SIZE + x] = value; }
        }

        public Chunk RelativeGetChunk(float inputX, float inputY)
        {
            //TODO Camera position (divided by 2)
            //map.x = (screen.x / TILE_WIDTH_HALF + screen.y / TILE_HEIGHT_HALF) /2;
            //map.y = (screen.y / TILE_HEIGHT_HALF -(screen.x / TILE_WIDTH_HALF)) /2;
            return this[(int)(((inputX / chunkSizeWidth) + (inputY / chunkSizeHeight))), (int)(((inputY / chunkSizeHeight) - (inputX / chunkSizeWidth)))];
        }
        public Block RelativeGetBlock(float inputX, float inputY)
        {
            Chunk c = RelativeGetChunk(inputX, inputY);
            float inputChunkRelativeX = inputX - c.X*chunkSizeWidth;
            float inputChunkRelativeY = inputY - c.Y*chunkSizeHeight;
            
            return c[(int)((inputChunkRelativeX / Block.BLOCK_WIDTH) + (inputChunkRelativeY / Block.BLOCK_HEIGHT)), (int)((inputChunkRelativeY / Block.BLOCK_HEIGHT) - (inputChunkRelativeX / Block.BLOCK_WIDTH))];
        }
    }
}

