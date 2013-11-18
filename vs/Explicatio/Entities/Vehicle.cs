using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Explicatio.Entities
{
    public class Vehicle
    {
        private float x;
        /// <summary>
        /// Pozycja X na świecie
        /// </summary>
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        private float y;
        /// <summary>
        /// Pozycja Y na świecie
        /// </summary>
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        private float width;
        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        private float height;
        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        private float rotation;
        /// <summary>
        /// Obrót pojazdu wyrażony w stopniach
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vehicle(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
