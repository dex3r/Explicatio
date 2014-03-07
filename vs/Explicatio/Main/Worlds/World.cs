using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Main.Worlds
{
    public class World
    {
        public const int WORLD_SIZE = 8;
        private Chunk[] chunks = new Chunk[WORLD_SIZE * WORLD_SIZE];

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

        public Chunk this[int x, int y]
        {
            get { return chunks[y * WORLD_SIZE + x]; }
            set { chunks[y * WORLD_SIZE + x] = value; }
        }
    }
}

