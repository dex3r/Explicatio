using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Worlds
{
    public class World
    {
        private Chunk[] chunks;
        /// <summary>
        /// Rozmiar świata w blokach
        /// </summary>
        public readonly int Size;
        /// <summary>
        /// Rozmiar jednego boku świata wyrażony w chunkach
        /// TODO: zmienić nazwę?
        /// </summary>
        public readonly int ChunksPerDimension;

        public World(int size)
        {
            this.Size = size;
            ChunksPerDimension = size / Chunk.CHUNK_SIZE;
            chunks = new Chunk[ChunksPerDimension * ChunksPerDimension];
            for (int xi = 0; xi < ChunksPerDimension; xi++)
            {
                for (int yi = 0; yi < ChunksPerDimension; yi++)
                {
                    this[xi, yi] = new Chunk(xi, yi);
                }
            }
        }

         //!? Properties region
        #region PROPERTIES

        #endregion
        //!? END of properties region

        public Chunk this[int x, int y]
        {
            get { return chunks[y * ChunksPerDimension + x]; }
            set { chunks[y * ChunksPerDimension + x] = value; }
        }

        //public Chunk RelativeGetChunk(float inputX, float inputY)
        //{
        //    //TODO Camera position (divided by 2)
        //    //map.x = (screen.x / TILE_WIDTH_HALF + screen.y / TILE_HEIGHT_HALF) /2;
        //    //map.y = (screen.y / TILE_HEIGHT_HALF -(screen.x / TILE_WIDTH_HALF)) /2;
        //    return this[(int)(((inputX / chunkSizeWidth) + (inputY / chunkSizeHeight))), (int)(((inputY / chunkSizeHeight) - (inputX / chunkSizeWidth)))];
        //}
        //public int RelativeGetBlock(float inputX, float inputY)
        //{
        //    Chunk c = RelativeGetChunk(inputX, inputY);
        //    float inputChunkRelativeX = inputX - c.X*chunkSizeWidth;
        //    float inputChunkRelativeY = inputY - c.Y*chunkSizeHeight;
            
        //    return c[(int)((inputChunkRelativeX / Block.BLOCK_WIDTH) + (inputChunkRelativeY / Block.BLOCK_HEIGHT)), (int)((inputChunkRelativeY / Block.BLOCK_HEIGHT) - (inputChunkRelativeX / Block.BLOCK_WIDTH))];
        //}
    }
}

