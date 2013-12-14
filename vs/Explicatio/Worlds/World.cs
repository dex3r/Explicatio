using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Explicatio.Entities;

namespace Explicatio.Worlds
{
    public class World
    {
        //TODO: pobieranie bloku cx metadanych
        private int size;
        public int Size
        {
            get { return size; }
        }

        /// <summary>
        /// Ilość wszystkich chunków dla obecnej wielkości świata
        /// </summary>
        public int ChunkNumbers { get; private set; }
        /// <summary>
        /// Ilość chunków w jednym rzędzie (pierwiastek z ChunkNumbers)
        /// </summary>
        public int ChunksInRow { get; private set; }

        private Chunk[] chunks;

        public World()
        {
            size = 256 + 64 + 16;
            //size = 1024;
            ChunkNumbers = (int)Math.Pow(size / Chunk.CHUNK_SIZE, 2);
            ChunksInRow = size / Chunk.CHUNK_SIZE;
            chunks = new Chunk[ChunkNumbers];
            for (int x = 0; x < ChunksInRow; x++)
            {
                for (int y = 0; y < ChunksInRow; y++)
                {
                    chunks[ChunksInRow * y + x] = new Chunk(this, x, y);
                }
            }
        }

        public Chunk GetChunk(int x, int y)
        {
            return chunks[ChunksInRow * y + x];
        }

        /// <summary>
        /// Przemieszcza pojazd w nowe miejsce
        /// UWAGA!
        /// Nie zmienia pól w obiekcie vehicle
        /// </summary>
        /// <param name="vehicle">Pojazd do przesunięcia</param>
        /// <param name="cx">Nowa pozycja X</param>
        /// <param name="cy">Nowa pozycja Y</param>
        public void MoveVehicle(Vehicle vehicle, float x, float y)
        {
            //!? Zmienić pozycję pojazdu w chunku, zmienić chunk jeżeli konieczne
            throw new NotImplementedException();
        }
    }
}
