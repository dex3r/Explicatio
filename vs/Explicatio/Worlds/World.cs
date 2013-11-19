using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Explicatio.Worlds
{
    public class World
    {
        //TODO: Lista chunków, pobieranie bloku i metadanych
        const int WORLDSIZE = 128*128; //Wysokość i szerokość
        List<Chunk> chunk = new List<Chunk>(); //Lista wszystich chunków
        public World()
        {
            for (int x = 0; x <= WORLDSIZE/2; x++)
            {
                for (int y = 0; y <= WORLDSIZE/2; y++)
                {
                    chunk.Add(new Chunk());
                }
            }
        }
        public Chunk Chunk(int id)
        {
            return chunk[id];
        }
    }
}
