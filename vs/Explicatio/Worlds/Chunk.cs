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
        public const byte CHUNK_SIZE = 16;

        /// <summary>
        ///  ID obiektów, tablice jednowymiarowe dla wydajności
        /// </summary>
        private byte[] chunkGround = new byte[CHUNK_SIZE * CHUNK_SIZE];

        private byte[] chunkGroundMeta = new byte[CHUNK_SIZE * CHUNK_SIZE];
        /// <summary>
        ///  Metadane obiektów
        /// </summary>
        public byte[] ChunkGroundMeta
        {
            get { return chunkGroundMeta; }
        }

        private List<Property> properties;
        /// <summary>
        /// Lista wszystkich nieruchomości w tym chunku
        /// Gdy nieruchmość stoi na granicy (na dwóch chunkach) jej referencja jest w obu
        /// </summary>
        public List<Property> Properties
        {
            get { return properties; }
        }

        private List<Vehicle> vehicles;
        /// <summary>
        /// Lista wszystkich pojazdów w tym chunku
        /// TODO: Znaleźć wydajniesze rozwiązanie niż "List"
        /// </summary>
        public List<Vehicle> Vehicles
        {
            get { return vehicles; }
        }

        public Chunk()
        {
            vehicles = new List<Vehicle>();
            properties = new List<Property>();
            //? Domyślne wartości od razu wynoszą 0
            //ResetChunkData(0);
        }

        /// <summary>
        /// Reset wszystkich danych w chunku
        /// </summary>
        /// <param name="id">Id pola</param>
        public void ResetChunkData(byte id)
        {
            //! for jest szybsze dla typów nieiteracyjnych :)
            foreach (byte i in chunkGround)
            {
                chunkGround[i] = id;
                chunkGroundMeta[i] = 0;
            }
            vehicles.Clear();
            properties.Clear();
        }
        public byte this[ushort x, ushort y]
        {
            get { return chunkGround[CHUNK_SIZE * y + x]; }
            set { chunkGround[CHUNK_SIZE * y + x] = value; }
        }
    }
}
