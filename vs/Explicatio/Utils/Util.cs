using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Runtime.CompilerServices;

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
    }
}
