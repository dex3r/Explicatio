using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explicatio.Rendering;

namespace Explicatio.Worlds
{
    public class Chunk
    {
        public const int CHUNK_SIZE = 64;

        private ChunkRenderer chunkRenderer;
        private short[] blocks;
        //Chunk Coordinates
        private int x;
        private int y;

        //!? Properties region
        #region PROPERTIES
        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }
        public ChunkRenderer ChunkRenderer
        {
            get { return chunkRenderer; }
        }
        #endregion
        //!? END of properties region

        public Chunk(int x, int y)
        {
            this.x = x;
            this.y = y;
            blocks = new short[CHUNK_SIZE * CHUNK_SIZE];
            for (int xi = 0; xi < CHUNK_SIZE; xi++)
            {
                for (int yi = 0; yi < CHUNK_SIZE; yi++)
                {
                    this[xi, yi] = Block.Grass.Id;
                }
            }
            chunkRenderer = new ChunkRenderer();
        }
        /// <summary>
        /// Zwraca blok w chunku x,y to pozycja w tablicy chunka.
        /// </summary>
        /// <param name="x">Pozycja w tablicy</param>
        /// <param name="y">Pozycja w tablicy</param>
        public int this[int x,int y]
        {
            get { return blocks[y * CHUNK_SIZE + x]; }
            set
            {
                blocks[y * CHUNK_SIZE + x] = (short)value;
            }
        }
        ///// <summary>
        ///// Zwraca blok w chunku x,y to pozycja w tablicy chunka.
        ///// </summary>
        //public int this[int flattendPosition]
        //{
        //    get { return blocks[flattendPosition]; }
        //    set
        //    {
        //        blocks[flattendPosition] = (short)value;
        //    }
        //}

    }
}
