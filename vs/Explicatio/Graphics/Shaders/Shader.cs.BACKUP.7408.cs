﻿using OpenTK.Graphics.OpenGL;
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
<<<<<<< HEAD
        private static ShaderChunk2 chunk2Shader;
=======
>>>>>>> parent of fa5322c... Merge branch 'OpenTK' of https://github.com/dex3r/Explicatio into OpenTK

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
<<<<<<< HEAD
        public static ShaderChunk2 Chunk2Shader
        {
            get { return Shader.chunk2Shader; }
        }
=======

>>>>>>> parent of fa5322c... Merge branch 'OpenTK' of https://github.com/dex3r/Explicatio into OpenTK
        #endregion
        //!? END of static properties region

        public static void Init()
        {
            simpleColorShader = new ShaderSimpleColor("SimpleColor");
            simpleTextureShader = new ShaderSimpleTexture("SimpleTexture");
<<<<<<< HEAD
            //chunkShader = new ShaderChunk("Chunk", true);
            chunk2Shader = new ShaderChunk2("Chunk2");
=======
            chunkShader = new ShaderChunk("Chunk", true);
>>>>>>> parent of fa5322c... Merge branch 'OpenTK' of https://github.com/dex3r/Explicatio into OpenTK
        }

        #endregion

        private int shaderProgramHandle;
        private int vertexShaderHandle;
        private int fragmentShaderHandle;
        private int geometryShaderHandle;
        private string name;
        private Matrix4 projectionMatrix;
        private Matrix4 modelMatrix;
        private int projectionMatrixHandle;
        private int modelMatrixHandle;
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
                    if (RenderingManager.CurrentShader != this)
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
                    if (RenderingManager.CurrentShader != this)
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

            projectionMatrixHandle = GL.GetUniformLocation(ShaderProgramHandle, "projectionMatrix");
            modelMatrixHandle = GL.GetUniformLocation(ShaderProgramHandle, "modelMatrix");
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
            GL.UniformMatrix4(projectionMatrixHandle, false, ref projectionMatrix);
            GL.UniformMatrix4(modelMatrixHandle, false, ref modelMatrix);
        }

        public void SetPMAndUpdate(Matrix4 projectionMatrix, Matrix4 modelMatrix)
        {
            this.projectionMatrix = projectionMatrix;
            this.modelMatrix = modelMatrix;
            GL.UniformMatrix4(projectionMatrixHandle, false, ref projectionMatrix);
            GL.UniformMatrix4(modelMatrixHandle, false, ref modelMatrix);
        }
    }
}
