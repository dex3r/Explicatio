using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexGUI.Rendering
{
    public interface IRenderer
    {
        /// <summary>
        /// Should be called only once.
        /// </summary>
        void Initialize();
        /// <summary>
        /// Calling before actual GUI drawing.
        /// </summary>
        void PreDraw();
        /// <summary>
        /// Calling after all GUI elements has been drawn.
        /// </summary>
        void PostDraw();

    }
}
