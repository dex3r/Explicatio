using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexGUI
{
    public class GUIComponent : IComparable<GUIComponent>
    {
        public int DrawOrder { get; set; }
        public GUIScreen Owner { get; internal set; }



        public int CompareTo(GUIComponent other)
        {
            return DrawOrder - other.DrawOrder;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }
    }
}
