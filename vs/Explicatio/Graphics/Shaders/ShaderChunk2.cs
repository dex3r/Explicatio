using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Explicatio.Graphics.Shaders
{
    public class ShaderChunk2 : Shader
    {
        private Matrix4 projectionModelMatrix;
        private int projectionModelMatrixHandle;

         //!? Properties region
        #region PROPERTIES
        public Matrix4 ProjectionModelMatrix
        {
            get { return projectionModelMatrix; }
            set 
            {
                if (this.projectionModelMatrix != value)
                {
                    this.projectionModelMatrix = value;
                    if (RenderingManager.CurrentShader != this)
                    {
                        ShouldUpdateAllUniformsAtUse = true;
                    }
                    else
                    {
                        GL.UniformMatrix4(projectionModelMatrixHandle, false, ref this.projectionModelMatrix);
                    }
                }
            }
        }
        #endregion
        //!? END of properties region

        public ShaderChunk2(string name)
            : base(name)
        {
            projectionModelMatrixHandle = GL.GetUniformLocation(ShaderProgramHandle, "projectionModelMatrix");
        }
    }
}
