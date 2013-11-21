using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Explicatio.Worlds;
using System.Runtime.CompilerServices;

namespace Explicatio.Entities
{
    public class Vehicle : Updatable
    {
        private float x;
        /// <summary>
        /// Pozycja X na świecie
        /// </summary>
        public float X
        {
            get { return x; }
            set 
            { 
                x = value;
                this.UpdatePosition();
            }
        }

        private float oldX;
        public float OldX
        {
            get { return oldX; }
        }

        private float y;
        /// <summary>
        /// Pozycja Y na świecie
        /// </summary>
        public float Y
        {
            get { return y; }
            set 
            { 
                y = value;
                this.UpdatePosition();
            }
        }

        private float oldY;
        public float OldY
        {
            get { return oldY; }
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

        private World currentWorld;
        /// <summary>
        /// Świat w którym znajduje się pojazd
        /// </summary>
        public World CurrentWorld
        {
            get { return currentWorld; }
        }

        public Vehicle(World world, int x, int y)
        {
            this.currentWorld = world;
            this.x = x;
            this.y = y;
        }

        public void Update(GameTime gameTime)
        {
            this.oldX = x;
            this.oldY = y;
        }

        public bool Intersect(float x, float y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Przemieść pojazd
        /// </summary>
        /// <param name="cx">X wektora przemieszczenia</param>
        /// <param name="cy">Y wektora przemieszczenia</param>
        public void Move(float x, float y)
        {
            this.x += x;
            this.y += y;
            UpdatePosition();
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdatePosition()
        {
            currentWorld.MoveVehicle(this, this.x, this.y);
        }
    }
}
