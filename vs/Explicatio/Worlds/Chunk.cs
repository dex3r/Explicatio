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

        private blockData[] blocks;
        public struct blockData
        {
            public short id;
            public short meta;
            public short height;
        }
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
            blocks = new blockData[CHUNK_SIZE * CHUNK_SIZE];
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
        public blockData this[int x, int y]
        {
            get { return blocks[y * CHUNK_SIZE + x]; }
        }

        public short GetId(int x, int y)
        {
            return this[x, y].id;
        }
        public void SetId(int x, int y, short id)
        {
            blocks[y * CHUNK_SIZE + x].id = id;
            if(chunkRenderer != null)
            {
                chunkRenderer.SetUVs(x, y, id, this[x,y].meta);
            }
            else
            {
                rebuildChunk = true;
            }
        }

        public void SetIdAndMeta(int x, int y, short id, short meta)
        {
            SetId(x, y, id);
            SetMeta(x, y, meta);
        }
        public short GetMeta(int x, int y)
        {
            return this[x, y].meta;
        }
        public void SetMeta(int x, int y, short meta)
        {
            blocks[y * CHUNK_SIZE + x].meta = meta;
            if(chunkRenderer != null)
            {
                chunkRenderer.SetUVs(x, y, this[x,y].id, meta);
            }
            else
            {
                rebuildChunk = true;
            }
        }
        public short GetHeight(int x, int y)
        {
            return this[x, y].height;
        }
        public void SetHeight(int x, int y, byte height)
        {
            blocks[y * CHUNK_SIZE + x].height = height;
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
