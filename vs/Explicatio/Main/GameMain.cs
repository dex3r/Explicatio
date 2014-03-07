using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;
using Explicatio.Graphics;
using Explicatio.Utils;
using Explicatio.Graphics.Shaders;

namespace Explicatio.Main
{
    public static class GameMain
    {
        private static bool wasUpdated;

        private static int commonVertexArrayHandle;
        private static int commonVertexBufferHandle;

        private static int pyramidIndiecesBufferHandle;

        private static readonly byte[] pyramidIndices = new byte[] {
            3, 25, 7, 10, 3, 21, 25, 10
        };


        //!? Properties region
        #region PROPERTIES

        #endregion
        //!? END of properties region

        private static void GenerateVBOs()
        {
            // Common:
            commonVertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(commonVertexArrayHandle);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            commonVertexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, commonVertexBufferHandle);
            GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, new IntPtr(Primitive.VBODataCommon.Length * Vector2.SizeInBytes), Primitive.VBODataCommon, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            // Pyramid indices:
            pyramidIndiecesBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, pyramidIndiecesBufferHandle);
            GL.BufferData<byte>(BufferTarget.ElementArrayBuffer, new IntPtr(pyramidIndices.Length * sizeof(byte)), pyramidIndices, BufferUsageHint.StreamDraw);

            GL.BindVertexArray(0);
        }

        public static void Load(object sender, EventArgs e)
        {
           
            Util.PrintGLError("OnLoad");
            GenerateVBOs();
            Util.PrintGLError("GenerateVBOs");
            Shader.Init();
            Util.PrintGLError("Shaders init");
            Renderer.Init();
            Util.PrintGLError("Renderer init");
            GL.Enable(EnableCap.DepthTest);
            GL.FrontFace(FrontFaceDirection.Cw);
        }

        public static void Update(object sender, FrameEventArgs e)
        {
            wasUpdated = true;
        }

        public static void DrawPyramid()
        {
            GL.BindVertexArray(commonVertexArrayHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, pyramidIndiecesBufferHandle);
            GL.DrawElements(PrimitiveType.TriangleStrip, 8, DrawElementsType.UnsignedByte, 0);
        }


        public static void Draw(object sender, FrameEventArgs e)
        {
            if (!wasUpdated)
            {
                return;
            }
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            DrawPyramid();

            Util.PrintGLError("Render");

            Display.Instance.SwapBuffers();
            wasUpdated = false;

            //Quad.SetVertexArray();
            //Quad.Draw();
            //Quad.Draw();
        }



        public static void Main(String[] args)
        {
            wasUpdated = false;
            using (Display display = new Display())
            {
                display.Run();
            }
        }
    }
}
