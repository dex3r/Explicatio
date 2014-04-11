using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Runtime.CompilerServices;
using OpenTK;

namespace Explicatio.Utils
{
    public static class Util
    {
        public static void PrintGLError(string errorPlace = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            ErrorCode ec = GL.GetError();
            if (ec != ErrorCode.NoError)
            {
                Console.WriteLine("On " + caller + ":" + lineNumber + (errorPlace == "" ? "" : " Msg: " + errorPlace) + " GL error: " + ec.ToString());
#if DEBUG
                throw new Exception("GL error");
#endif
            }
        }

        public static bool IntersectRectangleRectangle(float x1, float y1, float width1,float height1, float x2, float y2, float width2, float height2)
        {
           if (x1 + width1 < x2) return false;
           if (y1 + height1 < y2) return false;
           if (x2 + width2 < x1) return false;
           if (y2 + height2 < y1) return false;
                return true;
        }
        public static bool IntersectPointRectangle(float x1, float y1, float x2, float y2, float width2, float height2)
        {
           if (x1 < x2) return false;
           if (y1 < y2) return false;
           if (x2 + width2 < x1) return false;
           if (y2 + height2 < y1) return false;
                return true;
        }
    }
}
