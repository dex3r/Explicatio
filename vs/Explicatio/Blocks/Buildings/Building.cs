using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Explicatio.Blocks
{
    public class Building
    {

        private ushort x;
        public ushort X
        {
            get { return x; }
            set { x = value; }
        }
        private ushort y;
        public ushort Y
        {
            get { return y; }
            set { y = value; }
        }
        private Point chunkPosition;
        public Point ChunkPosition
        {
            get { return chunkPosition; }
            set { chunkPosition = value; }
        }

        public Building(Point chunkPosition, ushort x, ushort y)
        {
            this.chunkPosition = chunkPosition;
            this.x = x;
            this.y = y;
        }
    }
}
