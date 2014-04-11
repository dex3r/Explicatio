using Explicatio.Controls;
using Explicatio.Main;
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


        /// <summary>
        /// Zwraca pozycję bloku w świecie
        /// </summary>
        /// <returns>[0] pozycja x bloku, [1] pozycja y bloku </returns>
        public int[] RelativeGetBlock(float inputX, float inputY)
        {
            //map.x = (screen.x / TILE_WIDTH + screen.y / TILE_HEIGHT);
            //map.y = (screen.y / TILE_HEIGHT -(screen.x / TILE_WIDTH));
            int mapX = (int)((inputX / Block.BLOCK_WIDTH) + (inputY / Block.BLOCK_HEIGHT));
            int mapY = (int)((inputY / Block.BLOCK_HEIGHT) - (inputX / Block.BLOCK_WIDTH));
            int[] c = {mapX,mapY};
            return c;
        }
        ///// <summary>
        ///// Zwraca pozycję bloku w świecie
        ///// </summary>
        ///// <returns>[0] pozycja x bloku, [1] pozycja y bloku </returns>
        //public bool CheckMapBoundry()
        //{
        //    float mapX = MyMouse.XRelative / Block.BLOCK_WIDTH + MyMouse.YRelative / Block.BLOCK_HEIGHT;
        //    float mapY = MyMouse.YRelative / Block.BLOCK_HEIGHT - MyMouse.XRelative / Block.BLOCK_WIDTH;
        //    return Utils.Util.IntersectPointRectangle(mapX, mapY, 0, 0, GameMain.CurrentWorld.Size, GameMain.CurrentWorld.Size);
            
        //}
        /// <summary>
        /// Zwraca pozycję chunk w świecie
        /// </summary>
        /// <returns>[0] pozycja x chunk, [1] pozycja y chunk </returns>
        public int[] RelativeGetChunk(float inputX, float inputY)
        {
            int[] c = RelativeGetBlock(inputX, inputY);
            c[0] = c[0] / Chunk.CHUNK_SIZE;
            c[1] = c[1] / Chunk.CHUNK_SIZE;
            return c;
        }

        /// <summary>
        /// Zwraca pozycję bloku w chunku
        /// </summary>
        /// <returns>[0] pozycja x bloku, [1] pozycja y bloku </returns>
        public int[] RelativeGetBlockChunk(float inputX, float inputY)
        {
            int[] c = RelativeGetBlock(inputX, inputY);
            int[] d = RelativeGetChunk(inputX, inputY); 
            c[0] = c[0] % Chunk.CHUNK_SIZE;
            c[1] = c[1] % Chunk.CHUNK_SIZE;
            int[] e = { c[0], c[1], d[0], d[1] };
            return e;
        }
    }
}

