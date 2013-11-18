using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Explicatio.Nie_wiem_jak_nazawc_folder_wiec_zmien_jego_nazwe_na_fajna;

namespace Explicatio.Map
{
    class Chunk
    {
        const byte size = 16;
        byte[] chunkGround = new byte[size * size];
        byte[] chunkGroundMeta = new byte[size * size];
        List<Estate> chunkEstate = new List<Estate>();
        List<Vehicle> chunkVehicle = new List<Vehicle>(); //Może jest coś wydajnijszego przy częstym zapisie i odczycie
        
        int x, y;
        public Chunk(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public byte this[ushort x, ushort y]
        {
            get { return chunkGround[size * y + x]; }
            set { chunkGround[size * y + x] = value; }
        }

        public void CreateVehicle(int alginedX,int alginedY,int id)
        {
            chunkVehicle.Add(new Vehicle(x * alginedX, y * alginedY, id));//Konstruktor może przepisanie z innego chunka
        }

        public int FindVehicle(int alginedX, int alginedY)
        {
            for(int i=0; i<chunkVehicle.Count; i++)
            {
                if (chunkVehicle[i].Postion(alginedX + x, alginedY + y))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
