using Explicatio.Utils;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Graphics
{
    public class Shader
    {
        public static void Create(string shaderString, ShaderType type, ref int shaderHandler)
        {
            using (StreamReader sw = new StreamReader(@""+shaderString))
            {
                shaderString = sw.ReadToEnd();
            }

            shaderHandler = GL.CreateShader(type);

            GL.ShaderSource(shaderHandler, shaderString);

            GL.CompileShader(shaderHandler);

            Util.PrintGLError("CreateShader compileShaders");

            Console.WriteLine("CreateShader "+type+" shader info: " + GL.GetShaderInfoLog(shaderHandler));
        }
    }
}
