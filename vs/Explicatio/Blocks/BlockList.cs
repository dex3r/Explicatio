using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Blocks
{
    public static class BlockList
    {
        public static Block Grass = new Block(BlocksTypeEnum.Grass, "Grass");
        public static BlockRoad Road = new BlockRoad(BlocksTypeEnum.Road, "Road");
    }
}
