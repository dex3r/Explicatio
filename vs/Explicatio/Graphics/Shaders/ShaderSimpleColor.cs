using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Explicatio.Graphics.Shaders
{
    public class ShaderSimpleColor : ShaderSimple
    {
        private Vector3 color;
        private int colorHandle;

         //!? Properties region
        #region PROPERTIES
        public Vector3 Color
        {
            get { return color; }
            set 
            {
                if (this.color != value)
                {
                    this.color = value;
                    if (RenderingManager.CurrentShader != this)
                    {
                        ShouldUpdateAllUniformsAtUse = true;
                    }
                    else
                    {
                        GL.Uniform3(colorHandle, ref this.color);
                    }
                }
            }
        }
        #endregion
        //!? END of properties region

        public ShaderSimpleColor(string name)
            : base(name)
        {
            colorHandle = GL.GetUniformLocation(ShaderProgramHandle, "inColor");
        }
    }
}
