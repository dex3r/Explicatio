using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Explicatio.Graphics.Shaders;

namespace Explicatio.Graphics.Primitives
{
    public class PrimitiveSingleTexture : Primitive
    {
        private int textureId;

         //!? Properties region
        #region PROPERTIES
        public int TextureId
        {
            get { return textureId; }
            set { textureId = value; }
        }
        #endregion
        //!? END of properties region

        public PrimitiveSingleTexture(PrimitiveType primitiveType, Vector2[] vertices, byte[] indices)
            : base(primitiveType, vertices)
        {
            defaultShader = Shader.SimpleColorShader;
            this.indices = indices;
        }

        public override void Draw()
        {
            base.Draw();
            //((ShaderSimpleColor)defaultShader).Color = color;
            //GL.BindVertexArray(Primitive.CommonVertexArrayHandle);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, indiecesBufferHandle);
            //GL.DrawElements(primitiveType, indices.Length, DrawElementsType.UnsignedByte, 0);
        }

    }
}
