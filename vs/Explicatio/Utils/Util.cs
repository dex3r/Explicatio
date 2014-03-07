using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Explicatio.Utils
{
    public static class Util
    {
        public static void PrintGLError(string errorPlace)
        {
            ErrorCode ec = GL.GetError();
            if (ec != ErrorCode.NoError)
            {
                Console.WriteLine("On " + errorPlace + " GL error: " + ec.ToString());
#if DEBUG
                throw new Exception("GL error");
#endif
            }
        }
    }
}
