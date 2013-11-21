using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Explicatio.Entities;

namespace Explicatio.Worlds
{
    public class Chunk
    {
        /// <summary>
        /// Rozmiar chunku, "const" dla wydajności
        /// </summary>
        public const int CHUNK_SIZE = 32;

        /// <summary>
        ///  ID obiektów, tablice jednowymiarowe dla wydajności
        /// </summary>
        private byte[] chunkGround;

        private byte[] chunkGroundMeta;
        /// <summary>
        ///  Metadane obiektów
        /// </summary>
        public byte[] ChunkGroundMeta
        {
            get { return chunkGroundMeta; }
        }

        private List<Property>[] properties;
        /// <summary>
        /// Lista wszystkich nieruchomości w tym chunku
        /// Gdy nieruchmość stoi na granicy (na dwóch chunkach) jej referencja jest w obu
        /// Jednowymiarowa, CHUNK_SIZE elementowa tablica List w celach optymalizacyjnych
        /// Index to X
        /// </summary>
        public List<Property>[] Properties
        {
            get { return properties; }
        }

        private List<Vehicle>[] vehicles;
        /// <summary>
        /// Lista wszystkich pojazdów w tym chunku
        /// Jednowymiarowa, CHUNK_SIZE elementowa tablica List w celach optymalizacyjnych
        /// Index to X
        /// </summary>
        public List<Vehicle>[] Vehicles
        {
            get { return vehicles; }
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public World WorldObj { get; private set; }

        public byte this[ushort x, ushort y]
        {
            get { return chunkGround[CHUNK_SIZE * y + x]; }
            set { chunkGround[CHUNK_SIZE * y + x] = value; }
        }

        public Chunk(World world, int x, int y)
        {
            this.WorldObj = world;
            this.X = x;
            this.Y = y;
            vehicles = new List<Vehicle>[CHUNK_SIZE];
            properties = new List<Property>[CHUNK_SIZE];
            for(int i = 0; i < CHUNK_SIZE; i++)
            {
                vehicles[i] = new List<Vehicle>();
                properties[i] = new List<Property>();
            }
           
            // inizjalizacja pól w konstruktorze - dobra praktyka, ciężej pominąć x w przypadku 
            chunkGround = new byte[CHUNK_SIZE * CHUNK_SIZE];
            chunkGroundMeta = new byte[CHUNK_SIZE * CHUNK_SIZE];

            ResetChunkData(1);
        }

        /// <summary>
        /// Reset wszystkich danych w chunku
        /// </summary>
        /// <param name="id">Id pola</param>
        public void ResetChunkData(byte id)
        {
            //! for jest szybsze dla typów nieiteracyjnych :)
            for (int i = 0; i < CHUNK_SIZE * CHUNK_SIZE; i++ )
            {
                chunkGround[i] = id;
                chunkGroundMeta[i] = 0;
            }
            //TODO: Podmienić
            //vehicles.Clear();
            //properties.Clear();
        }
        
        public Vehicle GetVehicleAt(float x, float y)
        {
            int iX = (int)x;
            for(int i = 0; i < vehicles[iX].Count; i++)
            {
                if(vehicles[iX][i].Intersect(x, y))
                {
                    return vehicles[iX][i];
                }
            }
            return null;
        }
    }
}
