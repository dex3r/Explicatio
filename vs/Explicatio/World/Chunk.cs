using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Explicatio.Entities;

namespace Explicatio.World
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
        ///  Metadane obiektu
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

        private int y;
        public int Y
        {
            get { return y; }
        }
        private int x;
        public int X
        {
            get { return x; }
        }

        public Chunk(int x, int y)
        {
            this.x = x;
            this.y = y;
            vehicles = new List<Vehicle>();
            properties = new List<Property>();
        }

        public Chunk(int x, int y, List<Vehicle> vehicles, List<Property> properties)
        {
            this.x = x;
            this.y = y;
            this.vehicles = vehicles;
            this.properties = properties;
        }

        public byte this[ushort x, ushort y]
        {
            get { return chunkGround[CHUNK_SIZE * y + x]; }
            set { chunkGround[CHUNK_SIZE * y + x] = value; }
        }
    }
}
