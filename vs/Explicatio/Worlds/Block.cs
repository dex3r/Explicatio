using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Worlds
{
    public class Block
    {
        public const int BLOCK_WIDTH = 128;
        public const int BLOCK_HEIGHT = 64;

        private byte id;
        private byte blockMetaData;
        private string name;
        //!? Properties region
        #region PROPERTIES
        public byte Id
        {
            get { return id; }
            set { id = value; }
        }
        public byte BlockMetaData
        {
            get { return blockMetaData; }
            set { blockMetaData = value; }
        }
        public string Name
        {
            get { return name; }
        }
        #endregion
        //!? END of properties region

        public Block(byte id, byte meta)
        {
            this.id = id;
            this.blockMetaData = meta;
            //this.name = name;
        }

    }
}
