using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Explicatio.Nie_wiem_jak_nazawc_folder_wiec_zmien_jego_nazwe_na_fajna
{
    class Vehicle
    {
        int x, y, width, height;
        float rotation;
        Rectangle rect;
        public Vehicle(int x,int y, int id) //id to metadata dla samochodu (kolor, tekstura itd) 
        {
            this.x = x;
            this.y = y;

            rect = new Rectangle(x, y, width, height);
        }
        private Rectangle Rotation
        {
            //TODO Rotation
        }
        public bool Postion(int x,int y)
        {
            if((x<= rect.Left && x >= rect.Right) && ( y<=rect.Top && y>=rect.Bottom))
            {
                return true;
            }
            return false;
        }

    }
}
