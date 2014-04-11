using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Worlds
{
    public class Block
    {
        #region const
        public const int BLOCK_WIDTH = 64;
        public const int BLOCK_HEIGHT = 32;
        #endregion

        #region static

        private static Block[] blocks = new Block[256];
        public static Block[] Blocks
        {
            get { return Block.blocks; }
        }

        public static Block Grass = new Block(1, 0, "Grass");
        public static Block Road = new Block(2, 0, "Road");

        #endregion

        #region nonstatic
        private int id;
        private string name;
        //!? Properties region
        #region PROPERTIES
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
        }
        #endregion
        //!? END of properties region

        /// <summary>
        /// Ostrożnie z tym, ID zaczyna sie od 16 miejsca
        /// </summary>
        /// <param name="flagID"></param>
        /// <param name="name"></param>
        public Block(int flagID, string name)
        {
            this.id = flagID;
            this.name = name;
        }
        public Block(short id, byte meta, string name)
        {
            this.id = (id << 16) | (meta << 8);
            this.name = name;
        }
        #endregion
    }
}
