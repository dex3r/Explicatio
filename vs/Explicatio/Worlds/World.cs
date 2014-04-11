using Explicatio.Controls;
using Explicatio.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

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

        ///// <summary>
        ///// Zwraca pozycję bloku w świecie
        ///// </summary>
        ///// <returns>[0] pozycja x bloku, [1] pozycja y bloku </returns>
        public bool CheckMapBoundry()
        {
            return Utils.Util.IntersectPointRectangle(MyMouse.XWorld, MyMouse.YWorld, 0, 0, GameMain.CurrentWorld.Size, GameMain.CurrentWorld.Size);
        }
            
        //}
        /// <summary>
        /// Zwraca pozycję chunk w świecie
        /// </summary>
        /// <returns>[0] pozycja x chunk, [1] pozycja y chunk </returns>
        public Vector2 RelativeGetChunk()
        {
            return new Vector2(MyMouse.XWorld / Chunk.CHUNK_SIZE, MyMouse.YWorld / Chunk.CHUNK_SIZE);
        }

        /// <summary>
        /// Zwraca pozycję bloku w chunku
        /// </summary>
        /// <returns>[0] pozycja x bloku, [1] pozycja y bloku </returns>
        public Vector2 RelativeGetBlockChunk()
        {
            return new Vector2(MyMouse.XWorld % Chunk.CHUNK_SIZE, MyMouse.YWorld % Chunk.CHUNK_SIZE);
        }
        public bool SetBlock(int x, int y, int id, int meta = 0)
        {
            if(x < 0 || y < 0)
            {
                return false;
            }
            int cx = x / Chunk.CHUNK_SIZE;
            int cy = y / Chunk.CHUNK_SIZE;
            if(cx > ChunksPerDimension || cy > ChunksPerDimension)
            {
                return false;
            }
            else
            {
                this[cx, cy].SetIdAndMeta(x % Chunk.CHUNK_SIZE, y % Chunk.CHUNK_SIZE, id, meta);
                //this[cx, cy].ChunkRenderer = new Rendering.ChunkRenderer(this[cx, cy]);
                return true;
            }
        }
    }
}

