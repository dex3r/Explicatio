using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explicatio.Utils;
using OpenTK;

namespace Explicatio.Graphics.Shaders
{
    public class Shader
    {
        #region STATIC
        private static ShaderSimpleColor simpleColorShader;
        private static ShaderSimpleTexture simpleTextureShader;
        private static ShaderChunk chunkShader;
        private static Shader chunk2Shader;

        //!? Static properties region
        #region PROPERTIES
        public static ShaderSimpleColor SimpleColorShader
        {
            get { return Shader.simpleColorShader; }
        }
        public static ShaderSimpleTexture SimpleTextureShader
        {
            get { return Shader.simpleTextureShader; }
        }
        public static ShaderChunk ChunkShader
        {
            get { return Shader.chunkShader; }
        }
        public static Shader Chunk2Shader
        {
            get { return Shader.chunk2Shader; }
        }
        #endregion
        //!? END of static properties region

        public static void Init()
        {
            simpleColorShader = new ShaderSimpleColor("SimpleColor");
            simpleTextureShader = new ShaderSimpleTexture("SimpleTexture");
            //chunkShader = new ShaderChunk("Chunk", true);
            chunk2Shader = new Shader("Chunk2");
        }

        #endregion

        private int shaderProgramHandle;
        private int vertexShaderHandle;
        private int fragmentShaderHandle;
        private int geometryShaderHandle;
        private string name;
        protected bool shouldUpdateAllUniformsAtUse;

        //!? Properties region
        #region PROPERTIES
        public int ShaderProgramHandle
        {
            get { return shaderProgramHandle; }
        }
        
        public bool ShouldUpdateAllUniformsAtUse
        {
            get { return shouldUpdateAllUniformsAtUse; }
            set { shouldUpdateAllUniformsAtUse = value; }
        }
        #endregion
        //!? END of properties region

        protected Shader(string name, bool includeGeometryShader = false)
        {
            this.name = name;
            Create(includeGeometryShader);
        }

        private void Create(bool includeGeometryShader)
        {
            string sourceVS;
            string sourceFS;
            string sourceGS = "";
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

            shaderProgramHandle = GL.CreateProgram();

            GL.AttachShader(shaderProgramHandle, vertexShaderHandle);
            GL.AttachShader(shaderProgramHandle, fragmentShaderHandle);

            if (includeGeometryShader)
            {
                using (StreamReader sw = new StreamReader(@"Shaders/" + name + "GS.glsl"))
                {
                    sourceGS = sw.ReadToEnd();
                }
                geometryShaderHandle = GL.CreateShader(ShaderType.GeometryShader);
                GL.ShaderSource(geometryShaderHandle, sourceGS);
                GL.CompileShader(geometryShaderHandle);
                Util.PrintGLError("Shader " + name + " geometry: CreateShader");
                Console.WriteLine("CreateShaders " + name + " geometry shader info: " + GL.GetShaderInfoLog(geometryShaderHandle));
                GL.AttachShader(shaderProgramHandle, geometryShaderHandle);
            }

            GL.LinkProgram(shaderProgramHandle);
            Util.PrintGLError("Shader " + name + ": LinkProgram");
            Console.WriteLine("CreateShaders " + name + " program info: " + GL.GetProgramInfoLog(shaderProgramHandle));
            GL.UseProgram(shaderProgramHandle);
            Util.PrintGLError("Shader " + name + ": UseProgram");
        }

        /// <summary>
        /// Czy na pewno nie chcesz użyć <see cref="RenderingManager.ChangeCurrentShader"/>?
        /// Użyj tego shadera (UseProgram)
        /// </summary>
        public void Use()
        {
            GL.UseProgram(shaderProgramHandle);
            if (shouldUpdateAllUniformsAtUse)
            {
                UpdateAllUniforms();
            }
        }

        public virtual void UpdateAllUniforms()
        {

        }

        public virtual void SetPMAndUpdate(Matrix4 projectionMatrix, Matrix4 modelMatrix)
        {
            
        }
    }
}
