using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Explicatio.Entities;

namespace Explicatio.Worlds
{
    public class World
    {
        //TODO: Lista chunków, pobieranie bloku x metadanych
        const int WORLDSIZE = 128*128; //Wysokość x szerokość
        List<Chunk> chunk = new List<Chunk>(); //Lista wszystich chunków
        public World()
        {
            for (int x = 0; x <= WORLDSIZE/2; x++)
            {
                for (int y = 0; y <= WORLDSIZE/2; y++)
                {
                    //chunk.Add(new Chunk());
                }
            }
        }
        public Chunk Chunk(int id)
        {
            return chunk[id];
        }

        /// <summary>
        /// Przemieszcza pojazd w nowe miejsce
        /// UWAGA!
        /// Nie zmienia pól w obiekcie vehicle
        /// </summary>
        /// <param name="vehicle">Pojazd do przesunięcia</param>
        /// <param name="x">Nowa pozycja X</param>
        /// <param name="y">Nowa pozycja Y</param>
        public void MoveVehicle(Vehicle vehicle, float x, float y)
        {
            //!? Zmienić pozycję pojazdu w chunku, zmienić chunk jeżeli konieczne
            throw new NotImplementedException();
        }
    }
}
