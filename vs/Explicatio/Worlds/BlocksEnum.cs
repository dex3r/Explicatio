using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Worlds
{
    public enum BlocksEnum
    {
        //      North
        //  West / \ 
        //       \ / East
        //      South 
        Flat = 0,

        NorthSlope = 1,
        EastSlope = 2,
        NorthEastSlope = 3,
        SouthSlope = 4,
        NorthSouthSlope = 5,
        EastSouthSlope = 6,
        NorthEastSouthSlope = 7,
        WestSlope = 8,
        WestNorthSlope = 9,
        WestEastSlope = 10,
        WestNorthEastSlope = 11,
        SouthWestSlope = 12,
        SouthWestNorthSlope = 13,
        EastSouthWestSlope = 14,

        NorthEdge = 15,
        WestEdge = 16,
        SouthEdge = 17,
        EastEgde = 18,
    }
    public enum BlocksTypeEnum
    {
        Grass,
    }
}
