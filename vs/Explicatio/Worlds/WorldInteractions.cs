using Explicatio.Controls;
using Explicatio.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Worlds
{
    public static class WorldInteractions
    {
        public static void Mouse()
        {
            World w = GameMain.CurrentWorld;
            if(w.CheckMapBoundry())
            {
                int[] p =  w.RelativeGetBlockChunk(MyMouse.XRelative,MyMouse.YRelative); 
                //p[0] - blok x w chunku
                //p[1] - blok y w chunk
                //p[2] - chunk x
                //p[3] - chunk y
                w[p[2], p[3]].SetHeight(p[0], p[1], 10);
            }
        }

        public static void Keyboard()
        {
            throw new NotImplementedException();
        }
    }
}
