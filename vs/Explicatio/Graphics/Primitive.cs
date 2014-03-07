using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Explicatio.Graphics
{
    public class Primitive
    {
        public static readonly Vector2[] VBODataCommon = new Vector2[] {
            // Przód:
            new Vector2( 0.0f, 0.0f), // 0 środek
            new Vector2( 0.0f, 1.0f), // 1 góra
            new Vector2( 1.0f, 1.0f), // 2 góra prawo
            new Vector2( 1.0f, 0.0f), // 3 prawo
            new Vector2( 1.0f,-1.0f), // 4 dół prawo
            new Vector2( 0.0f,-1.0f), // 5 dół
            new Vector2(-1.0f,-1.0f), // 6 dół lewo
            new Vector2(-1.0f, 0.0f), // 7 lewo
            new Vector2(-1.0f, 1.0f)  // 8 góra lewo
        };
    }
}
