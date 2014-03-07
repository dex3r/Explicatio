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
        private static ShaderSimpleColor simpleColor;
        private static ShaderSimpleTexture simpleTexture;

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
            simpleTexture = new ShaderSimpleTexture("SimpleTexture");
        }

        #endregion

        private int shaderProgramHandle;
        private int vertexShaderHandle;
        private int fragmentShaderHandle;
        private string name;
        private Matrix4 projectionMatrix;
        private Matrix4 modelMatrix;
        private Matrix4 viewMatrix;
        private int projectionMatrixHandle;
        private int modelMatrixHandle;
        private int viewMatrixHandle;
        private bool shouldUpdateAllUniformsAtUse;

         //!? Properties region
        #region PROPERTIES
        public int ShaderProgramHandle
        {
            get { return shaderProgramHandle; }
        }
        public Matrix4 ProjectionMatrix
        {
            get { return this.projectionMatrix; }
            set
            {
                if (this.projectionMatrix != value)
                {
                    this.projectionMatrix = value;
                    if(Renderer.CurrentShader != this)
                    {
                        shouldUpdateAllUniformsAtUse = true;
                    }
                    else
                    {
                        GL.UniformMatrix4(projectionMatrixHandle, false, ref this.projectionMatrix);
                    }
                }
            }
        }
        public Matrix4 ModelMatrix
        {
            get { return this.modelMatrix; }
            set
            {
                if (this.modelMatrix != value)
                {
                    this.modelMatrix = value;
                    if (Renderer.CurrentShader != this)
                    {
                        shouldUpdateAllUniformsAtUse = true;
                    }
                    else
                    {
                        GL.UniformMatrix4(modelMatrixHandle, false, ref this.modelMatrix);
                    }
                }
            }
        }
        public Matrix4 ViewMatrix
        {
            get { return this.viewMatrix; }
            set
            {
                if (this.viewMatrix != value)
                {
                    this.viewMatrix = value;
                    if (Renderer.CurrentShader != this)
                    {
                        shouldUpdateAllUniformsAtUse = true;
                    }
                    else
                    {
                        GL.UniformMatrix4(viewMatrixHandle, false, ref this.viewMatrix);
                    }
                }
            }
        }
        public bool ShouldUpdateAllUniformsAtUse
        {
            get { return shouldUpdateAllUniformsAtUse; }
            set { shouldUpdateAllUniformsAtUse = value; }
        }
        #endregion
        //!? END of properties region
        
        protected Shader(string name)
        {
            this.name = name;
            Create();

            projectionMatrixHandle = GL.GetUniformLocation(ShaderProgramHandle, "projectionMatrix");
            modelMatrixHandle = GL.GetUniformLocation(ShaderProgramHandle, "modelMatrix");
            viewMatrixHandle = GL.GetUniformLocation(ShaderProgramHandle, "viewMatrix");
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
            if(shouldUpdateAllUniformsAtUse)
            {
                UpdateAllUniforms();
            }
        }

        public virtual void UpdateAllUniforms()
        {
            GL.UniformMatrix4(projectionMatrixHandle, false, ref projectionMatrix);
            GL.UniformMatrix4(modelMatrixHandle, false, ref modelMatrix);
            GL.UniformMatrix4(viewMatrixHandle, false, ref viewMatrix);
        }

        public void SetPMVAndUpdate(Matrix4 projectionMatrix, Matrix4 modelMatrix, Matrix4 viewMatrix)
        {
            this.projectionMatrix = projectionMatrix;
            this.modelMatrix = modelMatrix;
            this.viewMatrix = viewMatrix;
            GL.UniformMatrix4(projectionMatrixHandle, false, ref projectionMatrix);
            GL.UniformMatrix4(modelMatrixHandle, false, ref modelMatrix);
            GL.UniformMatrix4(viewMatrixHandle, false, ref viewMatrix);
        }
    }
}
