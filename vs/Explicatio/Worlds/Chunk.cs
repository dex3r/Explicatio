using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Worlds
{
    public class Chunk
    {
        public static readonly short CHUNK_SIZE = 32;

        //Array with Blocks
        private Block[] blocks = new Block[CHUNK_SIZE*CHUNK_SIZE];
        //Chunk Coordinates
        private int x;
        private int y;


        //!? Properties region
        #region PROPERTIES
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        #endregion
        //!? END of properties region

        public Chunk(bool defaultdata)
        {
            if (defaultdata)
            {
                for (int xi = 0; xi < CHUNK_SIZE; xi++)
                {
                    for (int yi = 0; yi < CHUNK_SIZE; yi++)
                    {
                        this[xi, yi] = new Block(1, 1);
                    }
                }
            }
            else throw new NotImplementedException("Here should be chunk loading");
        }
        /// <summary>
        /// Zwraca blok w chunku x,y to pozycja w tablicy chunka.
        /// </summary>
        /// <param name="x">Pozycja w tablicy</param>
        /// <param name="y">Pozycja w tablicy</param>
        public Block this[int x,int y]
        {
            get { return blocks[y * CHUNK_SIZE + x]; }
            set
            {
                blocks[y * CHUNK_SIZE + x] = value;
            }
        }

    }
}
