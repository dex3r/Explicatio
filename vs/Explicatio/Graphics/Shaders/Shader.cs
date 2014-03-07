using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explicatio.Utils;

namespace Explicatio.Graphics.Shaders
{
    public class Shader
    {
        #region STATIC
        private static ShaderSimpleColor simpleColor;

        //!? Static properties region
        #region PROPERTIES
        public static ShaderSimpleColor SimpleColor
        {
            get { return Shader.simpleColor; }
        }
        #endregion
        //!? END of static properties region

        public static void Init()
        {
            simpleColor = new ShaderSimpleColor("SimpleColor");
        }

        #endregion

        private int shaderProgramHandle;
        private int vertexShaderHandle;
        private int fragmentShaderHandle;
        private string name;

        private int[] primitivesHandles;

         //!? Properties region
        #region PROPERTIES
        public int ShaderProgramHandle
        {
            get { return shaderProgramHandle; }
        }
        #endregion
        //!? END of properties region
        
        protected Shader(string name)
        {
            this.name = name;
            Create();
        }

        private void Create()
        {
            string sourceVS;
            string sourceFS;
            using (StreamReader sw = new StreamReader(@"Shaders/" + name + "VS.glsl"))
            {
                sourceVS = sw.ReadToEnd();
            }
            using (StreamReader sw = new StreamReader(@"Shaders/" + name + "FS.glsl"))
            {
                sourceFS = sw.ReadToEnd();
            }

            vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            fragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vertexShaderHandle, sourceVS);
            GL.ShaderSource(fragmentShaderHandle, sourceFS);

            GL.CompileShader(vertexShaderHandle);
            Util.PrintGLError("Shader " + name + " vertex: CreateShader");
            GL.CompileShader(fragmentShaderHandle);
            Util.PrintGLError("Shader " + name + " fragmet: CreateShader");

            Console.WriteLine("CreateShaders " + name + " vertex shader info: " + GL.GetShaderInfoLog(vertexShaderHandle));
            Console.WriteLine("CreateShaders " + name + " fragment shader info: " + GL.GetShaderInfoLog(fragmentShaderHandle));

            // Create program
            shaderProgramHandle = GL.CreateProgram();

            GL.AttachShader(shaderProgramHandle, vertexShaderHandle);
            GL.AttachShader(shaderProgramHandle, fragmentShaderHandle);

            GL.LinkProgram(shaderProgramHandle);
            Util.PrintGLError("Shader " + name + " fragmet: LinkProgram");
            Console.WriteLine("CreateShaders " + name + " program info: " + GL.GetProgramInfoLog(shaderProgramHandle));
            GL.UseProgram(shaderProgramHandle);
            Util.PrintGLError("Shader " + name + " fragmet: UseProgram");
        }

        /// <summary>
        /// Czy na pewno nie chcesz użyć <see cref="Renderer.ChangeCurrentShader"/>?
        /// Użyj tego shadera (UseProgram)
        /// </summary>
        public void Use()
        {
            GL.UseProgram(shaderProgramHandle);
        }
    }
}
