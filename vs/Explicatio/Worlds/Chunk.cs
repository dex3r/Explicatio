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
        private int[] blocks;
        //Chunk Coordinates
        private int x;
        private int y;
        private bool rebuildChunk;

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
            set { chunkRenderer = value; }
        }
        public bool RebuildChunk
        {
            get { return rebuildChunk; }
            set { rebuildChunk = value; }
        }
        #endregion
        //!? END of properties region

        public Chunk(int x, int y)
        {
            this.x = x;
            this.y = y;
            Random r = new Random();
            blocks = new int[CHUNK_SIZE * CHUNK_SIZE];
            for (int xi = 0; xi < CHUNK_SIZE; xi++)
            {
                for (int yi = 0; yi < CHUNK_SIZE; yi++)
                {
                    SetId(xi, yi, 1);
                }
            }
            chunkRenderer = new ChunkRenderer(this);
        }
        /// <summary>
        /// Zwraca blok w chunku x,y to pozycja w tablicy chunka.
        /// </summary>
        /// <param name="x">Pozycja w tablicy</param>
        /// <param name="y">Pozycja w tablicy</param>
        public int this[int x,int y]
        {
            get { return blocks[y * CHUNK_SIZE + x]; }
            set { blocks[y * CHUNK_SIZE + x] = value; }
        }

        public ushort GetId(int x, int y)
        {
            return (ushort)((this[x,y] >> (2 << 3)) & 0xFFFF); // 0xFFFF = 65535
        }
        public void SetId(int x, int y, int id)
        {
            this[x, y] =  (id << 16) | (this[x, y] << 16) >> 16;
            if(chunkRenderer != null)
            {
                chunkRenderer.SetUVs(x, y, id, this[x, y] << 16);
            }
            else
            {
                rebuildChunk = true;
            }
        }

        public void SetIdAndMeta(int x, int y, int id, int meta)
        {
            SetId(x, y, id);
            SetMeta(x, y, meta);
        }
        public byte GetMeta(int x, int y)
        {
            return (byte)((this[x, y] >> (1 << 3)) & 0xFF); // 0xFF = 255
        }
        public void SetMeta(int x, int y, int meta)
        {
            this[x, y] = (this[x, y] << 16) | (meta << 8) | (this[x, y] << 24) >> 24;
            if(chunkRenderer != null)
            {
                chunkRenderer.SetUVs(x, y, (this[x, y] >> (2 << 3)) & 0xFFFF, meta);
            }
            else
            {
                rebuildChunk = true;
            }
        }
        public byte GetHeight(int x, int y)
        {
            return (byte)((this[x, y] >> (0 << 3)) & 0xFF); // 0xFF = 255
        }
        public void SetHeight(int x, int y, byte height)
        {
            this[x,y] = (this[x,y] << 8) | (height);
            if (chunkRenderer != null)
            {
                chunkRenderer.SetVertices(x, y, height);
            }
            else
            {
                rebuildChunk = true;
            }
        }
    }
}
