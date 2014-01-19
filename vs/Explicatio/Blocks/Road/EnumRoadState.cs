using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Blocks
{
    public enum EnumRoadState
    {
        corner_bottom = 0,
        corner_left,
        corner_right,
        corner_top ,
        end_left_bottom,
        end_left_top,
        end_right_bottom,
        end_right_top,
        intersection,
        straight_left,
        straight_right,
        T_left_bottom,
        T_left_top,
        T_right_bottom,
        T_right_top
    }
}
