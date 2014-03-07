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
    public class PrimitiveSingleColor : Primitive
    {
        private Vector3 color;

         //!? Properties region
        #region PROPERTIES
        public Vector3 Color
        {
            get { return color; }
            set { color = value; }
        }
        #endregion
        //!? END of properties region

        public PrimitiveSingleColor(PrimitiveType primitiveType)
            : base(primitiveType)
        {
            defaultShader = Shader.SimpleColor;
        }

        public PrimitiveSingleColor(PrimitiveType primitiveType, byte[] indices)
            : base(primitiveType)
        {
            defaultShader = Shader.SimpleColor;
            this.indices = indices;
        }

        public override void Draw()
        {
            base.Draw();
            ((ShaderSimpleColor)defaultShader).Color = color;
            GL.BindVertexArray(Primitive.CommonVertexArrayHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indiecesBufferHandle);
            GL.DrawElements(primitiveType, indices.Length, DrawElementsType.UnsignedByte, 0);
        }

    }
}
