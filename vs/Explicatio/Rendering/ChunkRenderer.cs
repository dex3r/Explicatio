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
        public const float METADATAS_PER_TEXTURE = 32f;
        public const float BLOCKS_PER_TEXTURE = 128f;
        private bool rebuferVertices;
        private bool rebuferUVs;

        private static readonly int[] VDataBlock = new int[] {
                 0,  0,
                 0,  2,
                 4,  0,
                 4,  0,
                 0,  2,
                 4,  2 
            };

        public int vertexArrayHandle;
        private int verticesBufferHandle;
        private int uvsBufferHandle;
        private float[] vertices;
        private float[] UVs;
        private Chunk ownerChunk;
        
#if DEBUG
        private bool isDisposed = false;
#endif

        public ChunkRenderer(Chunk c)
        {
            this.ownerChunk = c;
            vertices = new float[CHUNK_SIZE * CHUNK_SIZE * 12];
            UVs = new float[CHUNK_SIZE * CHUNK_SIZE * 12];

            vertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayHandle);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            Rebuild();
        }

        public void Rebuild()
        {
            for (int i = 0; i < CHUNK_SIZE; i++)
            {
                for (int j = 0; j < CHUNK_SIZE; j++)
                {
                    SetVertices(i, j, ownerChunk.GetHeight(i, j));
                    SetUVs(i, j, ownerChunk.GetId(i, j), ownerChunk.GetMeta(i, j));
                }
            }
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
                //GL.BufferSubData<float>(BufferTarget.ArrayBuffer, IntPtr.Zero, new IntPtr(sizeof(float) * UVs.Length), UVs);
                //GL.BufferSubData<float>(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * 12 * 6), new IntPtr(sizeof(float) * 12), UVs);
            }
            rebuferUVs = false;
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
            rebuferVertices = false;
        }

        public void Draw()
        {
            GL.BindVertexArray(vertexArrayHandle);
            if(verticesBufferHandle == 0 || ownerChunk.RebuildChunk)
            {
                RebufferUVs();
                RebufferVertices();
                ownerChunk.RebuildChunk = false;
            }
            else 
            {
                if (rebuferUVs)
                {
                    RebufferUVs();
                }
                if(rebuferVertices)
                {
                    RebufferVertices();
                }
            }
            
            GL.DrawArrays(PrimitiveType.Triangles, 0, CHUNK_SIZE * CHUNK_SIZE * 6);
        }

        public void ClearBuffers()
        {
            GL.DeleteBuffer(verticesBufferHandle);
            GL.DeleteBuffer(uvsBufferHandle);
            verticesBufferHandle = uvsBufferHandle = 0;
        }

        public void SetUVs(int x, int y, int blockID, int blockMeta)
        {
            int w = (x * 12) + (y * 12 * CHUNK_SIZE);
            float xOffset = (float)blockMeta / METADATAS_PER_TEXTURE;
            float yOffset = (float)(blockID) / BLOCKS_PER_TEXTURE;
            float maxXOffset = (float)(blockMeta + 1) / METADATAS_PER_TEXTURE;
            float maxYOffset = (float)(blockID - 1) / BLOCKS_PER_TEXTURE;
            UVs[w + 0] = xOffset;
            UVs[w + 1] = yOffset;
            UVs[w + 2] = xOffset;
            UVs[w + 3] = maxYOffset;
            UVs[w + 4] = maxXOffset;
            UVs[w + 5] = yOffset;
            UVs[w + 6] = maxXOffset;
            UVs[w + 7] = yOffset;
            UVs[w + 8] = xOffset;
            UVs[w + 9] = maxYOffset;
            UVs[w + 10] = maxXOffset;
            UVs[w + 11] = maxYOffset;
            this.rebuferUVs = true;
        }

        public void SetVertices(int x, int y, int height)
        {
            float px = -(x + height - y) * 2;
            float py = -(x + y);

            int w = (x * 12) + (y * 12 * CHUNK_SIZE);
            vertices[w + 0] = VDataBlock[0] + px;
            vertices[w + 1] = VDataBlock[1] + py;
            vertices[w + 2] = VDataBlock[2] + px;
            vertices[w + 3] = VDataBlock[3] + py;
            vertices[w + 4] = VDataBlock[4] + px;
            vertices[w + 5] = VDataBlock[5] + py;
            vertices[w + 6] = VDataBlock[6] + px;
            vertices[w + 7] = VDataBlock[7] + py;
            vertices[w + 8] = VDataBlock[8] + px;
            vertices[w + 9] = VDataBlock[9] + py;
            vertices[w + 10] = VDataBlock[10] + px;
            vertices[w + 11] = VDataBlock[11] + py;
            this.rebuferVertices = true;
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
            ownerChunk.ChunkRenderer = null;
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
