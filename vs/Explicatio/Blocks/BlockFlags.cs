using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Blocks
{
    [Flags]
    enum RoadMeta
    {
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,

    }
}
