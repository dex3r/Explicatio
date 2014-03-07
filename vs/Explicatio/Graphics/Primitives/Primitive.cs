using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Explicatio.Graphics.Shaders;
using OpenTK.Graphics.OpenGL;

namespace Explicatio.Graphics.Primitives
{
    public class Primitive
    {
        #region STATIC
        private static List<Primitive> allPrimitives = new List<Primitive>(10);

        public static readonly PrimitiveSingleColor singleColorTriangle = new PrimitiveSingleColor(PrimitiveType.Triangles, new byte[] { 4, 1, 6 });
        public static readonly PrimitiveSingleColor singleColorQuad = new PrimitiveSingleColor(PrimitiveType.TriangleStrip, new byte[] { 2, 4, 8, 6});

        public static readonly Vector2[] VBODataCommon = new Vector2[] {
            // Przód:
            new Vector2( 0.0f, 0.0f), // 0 środek
            new Vector2( 0.0f, 1.0f), // 1 góra
            new Vector2( 1.0f, 1.0f), // 2 góra prawo
            new Vector2( 1.0f, 0.0f), // 3 prawo
            new Vector2( 1.0f,-1.0f), // 4 dół prawo
            new Vector2( 0.0f,-1.0f), // 5 dół
            new Vector2(-1.0f,-1.0f), // 6 dół lewo
            new Vector2(-1.0f, 0.0f), // 7 lewo
            new Vector2(-1.0f, 1.0f)  // 8 góra lewo
        };

        private static int commonVertexArrayHandle;
        private static int commonVertexBufferHandle;

         //!? Static properties region
        #region PROPERTIES
        public static int CommonVertexArrayHandle
        {
            get { return Primitive.commonVertexArrayHandle; }
        }
        public static int CommonVertexBufferHandle
        {
            get { return Primitive.commonVertexBufferHandle; }
        }
        #endregion
        //!? END of static properties region


        public static void InitPrimitives()
        {
            commonVertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(commonVertexArrayHandle);
            GL.EnableVertexAttribArray(0);
            commonVertexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, commonVertexBufferHandle);
            GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, new IntPtr(Primitive.VBODataCommon.Length * Vector2.SizeInBytes), Primitive.VBODataCommon, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            foreach (var p in allPrimitives)
            {
                p.Init();
            }
        }

        #endregion

        protected Shader defaultShader;
        protected byte[] indices;
        protected int indiecesBufferHandle;
        protected PrimitiveType primitiveType;

         //!? Properties region
        #region PROPERTIES
        public Shader DefaultShader
        {
            get { return defaultShader; }
        }
        public byte[] Indices
        {
            get { return indices; }
        }
        public int IndiecesBufferHandle
        {
            get { return indiecesBufferHandle; }
        }
        #endregion
        //!? END of properties region

        public Primitive(PrimitiveType primitiveType)
        {
            this.primitiveType = primitiveType;
            allPrimitives.Add(this);
        }

        public virtual void Init()
        {
            indiecesBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indiecesBufferHandle);
            GL.BufferData<byte>(BufferTarget.ElementArrayBuffer, new IntPtr(indices.Length * sizeof(byte)), indices, BufferUsageHint.StreamDraw);
        }

        public virtual void Draw()
        {
            RenderingManager.ChangeCurrentShader(DefaultShader, true);
        }
    }
}
