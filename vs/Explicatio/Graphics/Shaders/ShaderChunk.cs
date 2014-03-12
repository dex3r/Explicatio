using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Explicatio.Graphics.Shaders
{
    public class ShaderChunk : ShaderSimple
    {
        private int chunkBlocksHandle;

        //!? Properties region
        #region PROPERTIES
        public int ChunkBlocksHandle
        {
            get { return chunkBlocksHandle; }
        }

        #endregion
        //!? END of properties region

        public ShaderChunk(string name, bool includeGeometryShader)
            : base(name, includeGeometryShader)
        {
            chunkBlocksHandle = GL.GetUniformLocation(this.ShaderProgramHandle, "chunkBlocks");
        }
    }
}
