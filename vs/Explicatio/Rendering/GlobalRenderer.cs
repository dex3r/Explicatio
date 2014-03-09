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

namespace Explicatio.Rendering
{
    public static class GlobalRenderer
    {
        public static int[] blocksData = new int[6 * 6];
        public static int[] blocksData1 = new int[6 * 6];
        public static int[] blocksData2 = new int[6 * 6];
        public static int[] blocksData3 = new int[6 * 6];

        private static int pointVertexArrayHandle;
        private static int pointVertexBufferHandle;
        private static Vector2[] pointVector = new Vector2[] { new Vector2(0, 0) };

        public static void InitTemp()
        {
            pointVertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(pointVertexArrayHandle);
            pointVertexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, pointVertexBufferHandle);

            string filename = "Content/gfx/terrain.png";

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap(filename);
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public static void RenderChunk(Chunk c)
        {
            RenderingManager.ChangeCurrentShader(Shader.ChunkShader, true);
            RenderingManager.ModelMatrix = Matrix4.Identity;

            GL.BindVertexArray(Primitive.CommonVertexArrayHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Primitive.singleColorTriangle.indiecesBufferHandle);
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    RenderingManager.ModelMatrix = Matrix4.CreateTranslation((j - i) * 11.634f, (j + i) * 5.814f, 0);
                    //? TODO: dane z mapy
                    GL.Uniform1(Shader.ChunkShader.ChunkBlocksHandle, 6 * 6, blocksData);
                    GL.DrawElements(PrimitiveType.Triangles, Primitive.singleColorTriangle.Indices.Length, DrawElementsType.UnsignedByte, 0);
                }
            }
        }
    }
}
