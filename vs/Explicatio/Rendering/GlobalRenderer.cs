using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Explicatio.Worlds;
using Explicatio.Graphics.Shaders;
using Explicatio.Graphics;
using Explicatio.Graphics.Primitives;
using Explicatio.Utils;

namespace Explicatio.Rendering
{
    public static class GlobalRenderer
    {
        const int CHUNK_SIZE = 64;
        const int WORLD_SIZE = 1024;
        const int WORLD_DIM = WORLD_SIZE / CHUNK_SIZE;

        //private static float[] VDataBlock = new float[] {
        //        -1.0f,  0.0f, // 0
        //         1.0f,  1.0f, // 2
        //         1.0f, -1.0f, // 4
        //         1.0f,  1.0f, // 2
        //         1.0f, -1.0f, // 4
        //         3.0f,  0.0f  // 6
        //    };

        //private static float[] VDataBlock = new float[] {
        //         0.0f,  0.0f,
        //         0.0f,  2.0f,
        //         4.0f,  0.0f,
        //         0.0f,  2.0f,
        //         4.0f,  0.0f,
        //         4.0f,  2.0f 
        //    };

        private static int[] VDataBlock = new int[] {
                 0,  0,
                 0,  2,
                 4,  0,
                 0,  2,
                 4,  0,
                 4,  2 
            };

        private class NChunk
        {
            public int VertexArrayHandle { get; private set; }
            private int verticesBufferHandle;
            private int uvsBufferHandle;
            int[] vertices = new int[CHUNK_SIZE * CHUNK_SIZE * 12];
            float[] UVs = new float[CHUNK_SIZE * CHUNK_SIZE * 12];

            public NChunk()
            {
                VertexArrayHandle = GL.GenVertexArray();
                GL.BindVertexArray(VertexArrayHandle);
                GL.EnableVertexAttribArray(0);
                GL.EnableVertexAttribArray(1);

                verticesBufferHandle = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, verticesBufferHandle);
                uvsBufferHandle = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, uvsBufferHandle);

                int x, y;
                int w;
                for (int k = 0; k < CHUNK_SIZE; k++)
                {
                    for (int l = 0; l < CHUNK_SIZE; l++)
                    {
                        //x = (l - k) * 1.939f;
                        //y = (l + k) * 0.969f;
                        //x = (l - k);
                        //y = (l + k);

                        x = (k - l) * 2;
                        y = (k + l);

                        w = (k * 12) + (l * 12 * CHUNK_SIZE);

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

                        //UVs[w + 0] = 0.0f;
                        //UVs[w + 1] = 0.5f;
                        //UVs[w + 2] = 0.5f;
                        //UVs[w + 3] = 1.0f;
                        //UVs[w + 4] = 0.5f;
                        //UVs[w + 5] = 0.0f;
                        //UVs[w + 6] = 0.5f;
                        //UVs[w + 7] = 1.0f;
                        //UVs[w + 8] = 0.5f;
                        //UVs[w + 9] = 0.0f;
                        //UVs[w + 10] = 1.0f;
                        //UVs[w + 11] = 0.5f;

                        UVs[w + 0] = 0.0f;
                        UVs[w + 1] = 0.0f;

                        UVs[w + 2] = 0f;
                        UVs[w + 3] = 1.0f;
                        UVs[w + 4] = 1f;
                        UVs[w + 5] = 0.0f;

                        UVs[w + 6] = 0f;
                        UVs[w + 7] = 1.0f;
                        UVs[w + 8] = 1f;
                        UVs[w + 9] = 0.0f;

                        UVs[w + 10] = 1.0f;
                        UVs[w + 11] = 1f;
                    }
                }

                GL.BindBuffer(BufferTarget.ArrayBuffer, verticesBufferHandle);
                GL.BufferData<int>(BufferTarget.ArrayBuffer, new IntPtr(sizeof(int) * vertices.Length), vertices, BufferUsageHint.DynamicDraw);
                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Int, false, 0, IntPtr.Zero);

                Util.PrintGLError();
            }

            public void renderChunk()
            {
                GL.BindVertexArray(VertexArrayHandle);

                GL.BindBuffer(BufferTarget.ArrayBuffer, uvsBufferHandle);
                GL.BufferData<float>(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * UVs.Length), ref UVs[0], BufferUsageHint.DynamicDraw);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);
            }
        }

        private static NChunk[] chunks;

        public static void InitTemp()
        {
            string filename = "Content/gfx/terrain.png";

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap(filename);
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            // We haven't uploaded mipmaps, so disable mipmapping (otherwise the texture will not appear).
            // On newer video cards, we can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
            // mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            //GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            Util.PrintGLError();

            chunks = new NChunk[WORLD_DIM * WORLD_DIM];

            for (int i = 0; i < WORLD_DIM; i++)
            {
                for (int j = 0; j < WORLD_DIM; j++)
                {
                    chunks[i * WORLD_DIM + j] = new NChunk();
                    chunks[i * WORLD_DIM + j].renderChunk();
                }
            }
            Util.PrintGLError();
        }

        public static void RenderAllChunks()
        {
            RenderingManager.ChangeCurrentShader(Shader.Chunk2Shader, false);
            NChunk c;
            for (int i = 0; i < 1; i++ )
            {
               chunks[i].renderChunk();
            }
            for (int i = 0; i < WORLD_DIM; i++)
            {
                for (int j = 0; j < WORLD_DIM; j++)
                {
                    //Shader.Chunk2Shader.ProjectionModelMatrix = Matrix4.CreateTranslation((j - i) * 30.9f, (j + i) * 15.46f, 0) * RenderingManager.ProjectionMatrix;
                    Shader.Chunk2Shader.ProjectionModelMatrix = Matrix4.CreateTranslation((j - i) * (CHUNK_SIZE * 2), (j + i) * CHUNK_SIZE, 0) * RenderingManager.ProjectionMatrix;
                    c = chunks[i * WORLD_DIM + j];
                    c.renderChunk();
                    GL.BindVertexArray(c.VertexArrayHandle);
                    GL.DrawArrays(PrimitiveType.Triangles, 0, CHUNK_SIZE * CHUNK_SIZE * 6);
                }
            }
        }
    }
}
