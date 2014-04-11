using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Explicatio.Worlds;
using Explicatio.Graphics.Shaders;
using Explicatio.Graphics;
using Explicatio.Graphics.Primitives;
using Explicatio.Utils;
using Explicatio.Main;

namespace Explicatio.Rendering
{
    public class ChunkRenderer : IDisposable
    {
        public const int CHUNK_SIZE = Chunk.CHUNK_SIZE;

        private static readonly int[] VDataBlock = new int[] {
                 0,  0,
                 0,  2,
                 4,  0,
                 4,  0,
                 0,  2,
                 4,  2 
            };

        private int vertexArrayHandle;
        private int verticesBufferHandle;
        private int uvsBufferHandle;
        private float[] vertices;
        private float[] UVs;
        
#if DEBUG
        private bool isDisposed = false;
#endif

        public ChunkRenderer(Chunk c)
        {
            vertices = new float[CHUNK_SIZE * CHUNK_SIZE * 12];
            UVs = new float[CHUNK_SIZE * CHUNK_SIZE * 12];

            vertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayHandle);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            float x, y, h;
            int w;
            for (int i = 0; i < CHUNK_SIZE; i++)
            {
                for (int j = 0; j < CHUNK_SIZE; j++)
                {
                    h = (float)c.GetHeight(i, j);
                    x = (i + h - j) * 2;
                    y = (i + j);

                    w = (i * 12) + (j * 12 * CHUNK_SIZE);

                    vertices[w + 0] = VDataBlock[0] + x;
                    vertices[w + 1] = VDataBlock[1] + y;
                    vertices[w + 2] = VDataBlock[2] + x;
                    vertices[w + 3] = VDataBlock[3] + y;
                    vertices[w + 4] = VDataBlock[4] + x;
                    vertices[w + 5] = VDataBlock[5] + y;
                    vertices[w + 6] = VDataBlock[6] + x;
                    vertices[w + 7] = VDataBlock[7] + y;
                    vertices[w + 8] = VDataBlock[8] + x;
                    vertices[w + 9] = VDataBlock[9] + y;
                    vertices[w + 10] = VDataBlock[10] + x;
                    vertices[w + 11] = VDataBlock[11] + y;

                    UVs[w + 0] = 0.0f;
                    UVs[w + 1] = 0.0f;
                    UVs[w + 2] = 0f;
                    UVs[w + 3] = 1f;
                    UVs[w + 4] = 1f;
                    UVs[w + 5] = 0.0f;
                    UVs[w + 6] = 1f;
                    UVs[w + 7] = 0.0f;
                    UVs[w + 8] = 0f;
                    UVs[w + 9] = 1f;
                    UVs[w + 10] = 1.0f;
                    UVs[w + 11] = 1f;
                }
            }

            Util.PrintGLError();
        }

        public void RebufferUVs()
        {
            if (uvsBufferHandle == 0)
            {
                GL.BindVertexArray(vertexArrayHandle);
                uvsBufferHandle = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, uvsBufferHandle);
                GL.BufferData<float>(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * UVs.Length), ref UVs[0], BufferUsageHint.DynamicDraw);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);
            }
            else
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, uvsBufferHandle);
                GL.BufferData<float>(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * UVs.Length), ref UVs[0], BufferUsageHint.DynamicDraw);
            }
            
        }

        public void RebufferVertices()
        {
            if (verticesBufferHandle == 0)
            {
                GL.BindVertexArray(vertexArrayHandle);
                verticesBufferHandle = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, verticesBufferHandle);
                GL.BufferData<float>(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * vertices.Length), vertices, BufferUsageHint.DynamicDraw);
                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);
            }
            else
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, verticesBufferHandle);
                GL.BufferData<float>(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * vertices.Length), vertices, BufferUsageHint.DynamicDraw);
            }
        }

        public void Draw()
        {
            GL.BindVertexArray(vertexArrayHandle);
            if(verticesBufferHandle == 0)
            {
                RebufferUVs();
                RebufferVertices();
            }
            GL.DrawArrays(PrimitiveType.Triangles, 0, CHUNK_SIZE * CHUNK_SIZE * 6);
        }

        public void ClearBuffers()
        {
            GL.DeleteBuffer(verticesBufferHandle);
            GL.DeleteBuffer(uvsBufferHandle);
            verticesBufferHandle = uvsBufferHandle = 0;
        }

        public void Dispose()
        {
            GL.DeleteBuffer(verticesBufferHandle);
            GL.DeleteBuffer(uvsBufferHandle);
            GL.DeleteVertexArray(vertexArrayHandle);
            vertexArrayHandle = uvsBufferHandle = vertexArrayHandle = 0;
#if DEBUG
            isDisposed = true;
#endif
        }

#if DEBUG
        ~ChunkRenderer()
        {
            if(!isDisposed && !GameMain.IsClosing)
            {
                throw new Exception("Possible GL content leak. Object has never been disposed!");
            }
        }
#endif
    }
}
