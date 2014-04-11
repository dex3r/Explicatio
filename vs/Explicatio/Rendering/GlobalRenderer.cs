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
using Explicatio.Main;

namespace Explicatio.Rendering
{
    public static class GlobalRenderer
    {
        public static void InitTemp()
        {
            string filename = "Content/gfx/terrain.png";

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap(filename);
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            //GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            Util.PrintGLError();
        }

        public static void RenderAllChunks()
        {
            RenderingManager.ChangeCurrentShader(Shader.Chunk2Shader, false);
            ChunkRenderer c;
            World w = GameMain.CurrentWorld;
            w[0, 0].chunkRenderer.Dispose();
            w[0, 0].chunkRenderer = new ChunkRenderer(w[0, 0]);
            for (int i = 0; i < w.ChunksPerDimension; i++)
            {
                for (int j = 0; j < w.ChunksPerDimension; j++)
                {
                    Shader.Chunk2Shader.ProjectionModelMatrix = Matrix4.CreateTranslation(-(j - i) * (Chunk.CHUNK_SIZE * 2), -(j + i) * Chunk.CHUNK_SIZE, 0) * RenderingManager.ProjectionMatrix;
                    c = w[i, j].ChunkRenderer;
                    //c.RebufferUVs();
                    c.Draw();
                }
            }
        }
    }
}
