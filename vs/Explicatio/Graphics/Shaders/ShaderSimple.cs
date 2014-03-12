using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Explicatio.Graphics.Shaders
{
    public class ShaderSimple : Shader
    {
        private Matrix4 projectionMatrix;
        private Matrix4 modelMatrix;
        private int projectionMatrixHandle;
        private int modelMatrixHandle;

         //!? Properties region
        #region PROPERTIES
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
        #endregion
        //!? END of properties region

        public ShaderSimple(string name, bool includeGeometryShader = false) : base(name, includeGeometryShader)
        {
            projectionMatrixHandle = GL.GetUniformLocation(ShaderProgramHandle, "projectionMatrix");
            modelMatrixHandle = GL.GetUniformLocation(ShaderProgramHandle, "modelMatrix");
        }

        public override void UpdateAllUniforms()
        {
            GL.UniformMatrix4(projectionMatrixHandle, false, ref projectionMatrix);
            GL.UniformMatrix4(modelMatrixHandle, false, ref modelMatrix);
        }

        public override void SetPMAndUpdate(Matrix4 projectionMatrix, Matrix4 modelMatrix)
        {
            this.projectionMatrix = projectionMatrix;
            this.modelMatrix = modelMatrix;
            GL.UniformMatrix4(projectionMatrixHandle, false, ref projectionMatrix);
            GL.UniformMatrix4(modelMatrixHandle, false, ref modelMatrix);
        }
    }
}
