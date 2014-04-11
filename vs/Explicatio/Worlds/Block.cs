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

        public static Block Grass = new Block(1, "Grass");
        public static Block Road = new Block(2, "Road");

        #endregion

        #region nonstatic
        private int id;
        private string name;
        private bool isNormalSizedBlock;

        //!? Properties region
        #region PROPERTIES
        public int Id
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
        }
        public bool IsNormalSizedBlock
        {
            get { return isNormalSizedBlock; }
        }
        #endregion
        //!? END of properties region

        public Block(int id, string name)
        {
            this.isNormalSizedBlock = true;
            this.id = id;
            this.name = name;
            blocks[id] = this;
        }
        #endregion
    }
}
