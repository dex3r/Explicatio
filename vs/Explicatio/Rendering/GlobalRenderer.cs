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
		const int CHUNK_SIZE = 16;
		const int WORLD_SIZE = 16;
		const int WORLD_DIM = WORLD_SIZE / CHUNK_SIZE;

		public static int[] blocksData = new int[CHUNK_SIZE * CHUNK_SIZE];
		public static int[] blocksData1 = new int[CHUNK_SIZE * CHUNK_SIZE];
		public static int[] blocksData2 = new int[CHUNK_SIZE * CHUNK_SIZE];
		public static int[] blocksData3 = new int[CHUNK_SIZE * CHUNK_SIZE];

		private static int pointVertexArrayHandle;
		private static int pointVertexBufferHandle;
		private static Vector2[] pointVector = new Vector2[] { new Vector2(0, 0) };


		private static float[] worldVertices = new float[WORLD_DIM * WORLD_DIM * CHUNK_SIZE * CHUNK_SIZE * 2 * 4];
		private static float[] worldUVs = new float[WORLD_DIM * WORLD_DIM * CHUNK_SIZE * CHUNK_SIZE * 2 * 4];
		private static int[] worldBuffers = new int[WORLD_DIM * WORLD_DIM * CHUNK_SIZE * CHUNK_SIZE];

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
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.NearestMipmapNearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);


			Util.PrintGLError();
			RenderingManager.ChangeCurrentShader(Shader.Chunk2Shader, false);
			Random r = new Random();
			
			float[] VDataBlock = new float[] {
				-1.0f,  0.0f, // 0
				 1.0f,  1.0f, // 2
				 1.0f, -1.0f, // 4
				 3.0f,  0.0f  // 6
			};

			//for (int i = 0; i < CHUNK_SIZE * CHUNK_SIZE; i++)
			//{
			//    blocksData[i] = r.Next(0, 3);
			//    blocksData1[i] = r.Next(0, 3);
			//    blocksData2[i] = r.Next(0, 3);
			//    blocksData3[i] = r.Next(0, 3);
			//}

		   // int t1;
			//int t2;
			//int t3;
			float x;
			float y;
			Matrix4 transf = RenderingManager.ProjectionMatrix;
			Vector4 v4;
			for (int i = 0; i < WORLD_DIM; i++)
			{
				for (int j = 0; j < WORLD_DIM; j++)
				{
					//t1 = ((i * WORLD_DIM * WORLD_DIM * CHUNK_SIZE * CHUNK_SIZE) + (j * WORLD_DIM * CHUNK_SIZE * CHUNK_SIZE));
					for(int k = 0; k < CHUNK_SIZE; k++)
					{
						//t2 = k * 8;
					   // t2 = t1 + (k * CHUNK_SIZE * CHUNK_SIZE);
						for(int l = 0; l < CHUNK_SIZE; l++)
						{
							x = (l - k) * 1.939f;
							y = (l + k) * 0.969f;
							//t3 = t1 + (t2 * CHUNK_SIZE + l);
							//t3 = t2 + (l * CHUNK_SIZE);

							int w = l * WORLD_DIM * WORLD_DIM * CHUNK_SIZE * CHUNK_SIZE + k * WORLD_DIM * CHUNK_SIZE * CHUNK_SIZE + j * CHUNK_SIZE * CHUNK_SIZE + i * CHUNK_SIZE;

							v4 = new Vector4(VDataBlock[0] + x, VDataBlock[1] + y, 0.0f, 1.0f);
							Vector4.Transform(ref v4, ref transf, out v4);
							worldVertices[w + 0] = v4.X;
							worldVertices[w + 1] = v4.Y;

							v4 = new Vector4(VDataBlock[2] + x, VDataBlock[3] + y, 0.0f, 1.0f);
							Vector4.Transform(ref v4, ref transf, out v4);
							worldVertices[w + 2] = v4.X;
							worldVertices[w + 3] = v4.Y;

							v4 = new Vector4(VDataBlock[4] + x, VDataBlock[5] + y, 0.0f, 1.0f);
							Vector4.Transform(ref v4, ref transf, out v4);
							worldVertices[w + 4] = v4.X;
							worldVertices[w + 5] = v4.Y;

							v4 = new Vector4(VDataBlock[6] + x, VDataBlock[7] + y, 0.0f, 1.0f);
							Vector4.Transform(ref v4, ref transf, out v4);
							worldVertices[w + 6] = v4.X;
							worldVertices[w + 7] = v4.Y;

							worldUVs[w + 0] = 0.0f;
							worldUVs[w + 1] = 0.5f;
							worldUVs[w + 2] = 0.5f;
							worldUVs[w + 3] = 0.1f;
							worldUVs[w + 4] = 0.5f;
							worldUVs[w + 5] = 0.0f;
							worldUVs[w + 6] = 1.0f;
							worldUVs[w + 7] = 0.5f;

							int wva = GL.GenVertexArray();
							GL.BindVertexArray(wva);
							GL.EnableVertexAttribArray(0);
							GL.EnableVertexAttribArray(1);

							Util.PrintGLError();
							int wvbh = GL.GenBuffer();
							GL.BindBuffer(BufferTarget.ArrayBuffer, wvbh);
							float[] blockVertices = new float[8];
							Array.Copy(worldVertices, w, blockVertices, 0, 8);
							GL.BufferData<float>(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * 8), blockVertices, BufferUsageHint.StaticDraw);
							Util.PrintGLError();
							GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);
							Util.PrintGLError();

							int wuvspb = GL.GenBuffer();
							GL.BindBuffer(BufferTarget.ArrayBuffer, wuvspb);
							float[] blockUVs = new float[8];
							Array.Copy(worldUVs, w, blockUVs, 0, 8);
							GL.BufferData<float>(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * 8), blockUVs, BufferUsageHint.StaticDraw);
							Util.PrintGLError();
							GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);
							Util.PrintGLError();

							worldBuffers[w / 8] = wva;
						}
					}
				}
			}
			Util.PrintGLError();
		}

		public static void RenderChunk(Chunk c)
		{
			RenderingManager.ChangeCurrentShader(Shader.Chunk2Shader, false);

			int t1;
			int t2;
			int t3;
			for (int i = 0; i < WORLD_DIM; i++)
			{
				for (int j = 0; j < WORLD_DIM; j++)
				{
					t1 = ((i * WORLD_DIM * WORLD_DIM * WORLD_DIM) + (j * WORLD_DIM * WORLD_DIM));
					for (int k = 0; k < CHUNK_SIZE; k++)
					{
						t2 = k;
						for (int l = 0; l < CHUNK_SIZE; l++)
						{
							t3 = t1 + (t2 * CHUNK_SIZE + l);
							GL.BindVertexArray(worldBuffers[t3]);
							GL.DrawArrays(PrimitiveType.Triangles, 0, 4);
						}
					}
				}
			}

			//RenderingManager.ChangeCurrentShader(Shader.ChunkShader, true);
			//RenderingManager.ModelMatrix = Matrix4.Identity;

			//GL.BindVertexArray(Primitive.CommonVertexArrayHandle);
			//GL.BindBuffer(BufferTarget.ElementArrayBuffer, Primitive.singleColorTriangle.indiecesBufferHandle);
			//Random r = new Random();
			//for (int i = 0; i < CHUNK_SIZE * CHUNK_SIZE; i++)
			//{
			//    blocksData[i] = r.Next(0, 3);
			//    blocksData1[i] = r.Next(0, 3);
			//    blocksData2[i] = r.Next(0, 3);
			//    blocksData3[i] = r.Next(0, 3);
			//}
			//int x;
			//for (int i = 0; i < 80; i++)
			//{
			//    for (int j = 0; j < 80; j++)
			//    {
			//        x = r.Next(0, 4);
			//        RenderingManager.ModelMatrix = Matrix4.CreateTranslation((j - i) * 11.634f, (j + i) * 5.814f, 0);
			//        unchecked
			//        {
			//            unsafe
			//            {
						   
			//                if (x == 0)
			//                {
			//                    fixed (int* bdp = &blocksData[0])
			//                    {
			//                        GL.Uniform1(Shader.ChunkShader.ChunkBlocksHandle, CHUNK_SIZE * CHUNK_SIZE, bdp);
			//                    }
			//                }
			//                else if (x == 1)
			//                {
			//                    fixed (int* bdp = &blocksData1[0])
			//                    {
			//                        GL.Uniform1(Shader.ChunkShader.ChunkBlocksHandle, CHUNK_SIZE * CHUNK_SIZE, bdp);
			//                    }
			//                }
			//                else if (x == 2)
			//                {
			//                    fixed (int* bdp = &blocksData2[0])
			//                    {
			//                        GL.Uniform1(Shader.ChunkShader.ChunkBlocksHandle, CHUNK_SIZE * CHUNK_SIZE, bdp);
			//                    }
			//                }
			//                else if (x == 3)
			//                {
			//                    fixed (int* bdp = &blocksData3[0])
			//                    {
			//                        GL.Uniform1(Shader.ChunkShader.ChunkBlocksHandle, CHUNK_SIZE * CHUNK_SIZE, bdp);
			//                    }
			//                }
			//            }
			//    }
			//        GL.DrawElements(PrimitiveType.Triangles, Primitive.singleColorTriangle.Indices.Length, DrawElementsType.UnsignedByte, 0);
			//    }
			//}
		}
	}
}
