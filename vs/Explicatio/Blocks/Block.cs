using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Explicatio.Worlds;
using Explicatio.Rendering;

namespace Explicatio.Blocks
{
    public class Block
    {
        #region Static

        private static Block[] blocks = new Block[256];
        public static Block[] Blocks
        {
            get { return Block.blocks; }
        }

        static Block()
        {
            blocks[0] = null;
            blocks[1] = new Block(1, "Grass", Textures.Textures.Grass);
            blocks[2] = new Block(2, "Snow", Textures.Textures.Snow);
            blocks[3] = new BlockRoad(3, "Road on grass");
        }

        #endregion
        #region Non-static

        private byte id;
        public byte Id
        {
            get { return id; }
        }

        private String name;
        public String Name
        {
            get { return name; }
        }

        /// <summary>
        /// Podstawowa tekstura dla tego bloku
        /// </summary>
        private Texture2D texture;

        public Block(byte id, String name)
        {
            this.id = id;
            this.name = name;
        }

        public Block(byte id, String name, Texture2D texture) : this(id, name)
        {
            this.texture = texture;
        }


        public virtual Texture2D GetTexture(Chunk c, ushort chunkX, ushort chunkY)
        {
            return texture;
        }

        public virtual void SetMeta(UInt16 value, World world, int x, int y)
        {
            world.SetMeta(value, x, y);
        }
        #endregion
    }
}
