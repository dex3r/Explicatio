using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Explicatio.Entities;
using Explicatio.Main;
using Explicatio.Rendering;

namespace Explicatio.Worlds
{
    public class Chunk
    {
        private bool[] mouseMeta;
        /// <summary>
        /// Czy chunk jest zaznaczony
        /// </summary>
        public bool[] MouseMeta
        {
            get { return mouseMeta; }
            set { mouseMeta = value; }
        }

        /// <summary>
        /// Rozmiar chunku, "const" dla wydajności
        /// </summary>
        public const int CHUNK_SIZE = 16;

        /// <summary>
        ///  ID obiektów, tablice jednowymiarowe dla wydajności
        /// </summary>
        private byte[] chunkGround;

        private UInt16[] chunkGroundMeta;
        /// <summary>
        ///  Metadane obiektów
        /// </summary>
        public UInt16[] ChunkGroundMeta
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
        /// <summary>
        /// Nie robić żadnych innych referencji do tego pola
        /// </summary>
        public RenderTarget2D RenderTarget { get; set; }

        private bool needsRedrawing;
        /// <summary>
        /// Zawsze zwraca true gdy RenderTarget jest pusty
        /// </summary>
        public bool NeedsRedrawing
        {
            get
            {
                return needsRedrawing;
            }
            set
            {
                needsRedrawing = value;
            }
        }

        public byte this[ushort x, ushort y]
        {
            get { return chunkGround[CHUNK_SIZE * y + x]; }
            set
            {
                chunkGround[CHUNK_SIZE * y + x] = value;
                MarkToRedraw();
            }
        }

        public UInt16 GetMeta(ushort x, ushort y)
        {
            return chunkGroundMeta[CHUNK_SIZE * y + x];
        }

        public void SetMeta(UInt16 value, ushort x, ushort y)
        {
            chunkGroundMeta[CHUNK_SIZE * y + x] = value;
            MarkToRedraw();
        }

        public Chunk(World world, int x, int y)
        {
            this.WorldObj = world;
            this.X = x;
            this.Y = y;
            vehicles = new List<Vehicle>[CHUNK_SIZE];
            properties = new List<Property>[CHUNK_SIZE];
            for (int i = 0; i < CHUNK_SIZE; i++)
            {
                vehicles[i] = new List<Vehicle>();
                properties[i] = new List<Property>();
            }

            chunkGround = new byte[CHUNK_SIZE * CHUNK_SIZE];
            chunkGroundMeta = new UInt16[CHUNK_SIZE * CHUNK_SIZE];
            mouseMeta = new bool[CHUNK_SIZE * CHUNK_SIZE];
            ResetChunkData(1);
        }

        /// <summary>
        /// Reset wszystkich danych w chunku
        /// </summary>
        /// <param name="id">Id pola</param>
        public void ResetChunkData(byte id)
        {
            for (int i = 0; i < CHUNK_SIZE * CHUNK_SIZE; i++)
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
            for (int i = 0; i < vehicles[iX].Count; i++)
            {
                if (vehicles[iX][i].Intersect(x, y))
                {
                    return vehicles[iX][i];
                }
            }
            return null;
        }

        public void MarkToRedraw()
        {
            NeedsRedrawing = true;
        }
    }
}
