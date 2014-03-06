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

namespace Explicatio.Main
{
    public static class GameMain
    {
        private static bool wasUpdated;

        private static readonly Vector3[] VBODataCommon = new Vector3[] {
            // Przód:
            new Vector3( 0.0f, 0.0f,-1.0f), // 0 środek
            new Vector3( 0.0f, 1.0f,-1.0f), // 1 góra
            new Vector3( 1.0f, 1.0f,-1.0f), // 2 góra prawo
            new Vector3( 1.0f, 0.0f,-1.0f), // 3 prawo
            new Vector3( 1.0f,-1.0f,-1.0f), // 4 dół prawo
            new Vector3( 0.0f,-1.0f,-1.0f), // 5 dół
            new Vector3(-1.0f,-1.0f,-1.0f), // 6 dół lewo
            new Vector3(-1.0f, 0.0f,-1.0f), // 7 lewo
            new Vector3(-1.0f, 1.0f,-1.0f), // 8 góra lewo
            // Sródek:
            new Vector3( 0.0f, 0.0f, 0.0f), // 9 środek
            new Vector3( 0.0f, 1.0f, 0.0f), // 10 góra
            new Vector3( 1.0f, 1.0f, 0.0f), // 11 góra prawo
            new Vector3( 1.0f, 0.0f, 0.0f), // 12 prawo
            new Vector3( 1.0f,-1.0f, 0.0f), // 13 dół prawo
            new Vector3( 0.0f,-1.0f, 0.0f), // 14 dół
            new Vector3(-1.0f,-1.0f, 0.0f), // 15 dół lewo
            new Vector3(-1.0f, 0.0f, 0.0f), // 16 lewo
            new Vector3(-1.0f, 1.0f, 0.0f), // 17 góra lewo
            // Tył:
            new Vector3( 0.0f, 0.0f, 1.0f), // 18 środek
            new Vector3( 0.0f, 1.0f, 1.0f), // 19 góra
            new Vector3( 1.0f, 1.0f, 1.0f), // 20 góra prawo
            new Vector3( 1.0f, 0.0f, 1.0f), // 21 prawo
            new Vector3( 1.0f,-1.0f, 1.0f), // 22 dół prawo
            new Vector3( 0.0f,-1.0f, 1.0f), // 23 dół
            new Vector3(-1.0f,-1.0f, 1.0f), // 24 dół lewo
            new Vector3(-1.0f, 0.0f, 1.0f), // 25 lewo
            new Vector3(-1.0f, 1.0f, 1.0f), // 26 góra lewo

        };
        private static readonly byte[] pyramidIndices = new byte[] {
            3, 25, 7, 10, 3, 21, 25, 10
        };

        private static int commonVertexArrayHandle;
        private static int commonVertexBufferHandle;

        private static int pyramidIndiecesBufferHandle;

        private static int shaderProgramHandle;
        private static int vertexShaderHandle;
        private static int fragmentShaderHandle;

        private static Matrix4 projectionMatrix;
        private static Matrix4 modelviewMatrix;
        private static int modelviewMatrixLocation;
        private static int projectionMatrixLocation;
        private static int colorVec3Location;

        //!? Properties region
        #region PROPERTIES
        public static int ShaderProgramHandle
        {
            get { return GameMain.shaderProgramHandle; }
        }
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
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, new IntPtr(VBODataCommon.Length * Vector3.SizeInBytes), VBODataCommon, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);

            // Pyramid indices:
            pyramidIndiecesBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, pyramidIndiecesBufferHandle);
            GL.BufferData<byte>(BufferTarget.ElementArrayBuffer, new IntPtr(pyramidIndices.Length * sizeof(byte)), pyramidIndices, BufferUsageHint.StreamDraw);

            GL.BindVertexArray(0);
        }

        private static void CreateShaders()
        {
            Shader.Create("./Shaders/SimpleVertexShader.glsl", ShaderType.VertexShader, ref vertexShaderHandle);
            Shader.Create("./Shaders/SimpleFragmentShader.glsl", ShaderType.FragmentShader, ref fragmentShaderHandle);

            // Create program
            shaderProgramHandle = GL.CreateProgram();

            Util.PrintGLError("CreateShaders createprogram");

            GL.AttachShader(shaderProgramHandle, vertexShaderHandle);
            GL.AttachShader(shaderProgramHandle, fragmentShaderHandle);

            //GL.BindAttribLocation(shaderProgramHandle, 1, "inPosition");
            //GL.BindAttribLocation(shaderProgramHandle, 0, "inColor");

            Util.PrintGLError("CreateShaders attachshader");

            GL.LinkProgram(shaderProgramHandle);
            Util.PrintGLError("CreateShaders link program");
            Console.WriteLine("CreateShaders shader program info: " + GL.GetProgramInfoLog(shaderProgramHandle));
            GL.UseProgram(shaderProgramHandle);
            Util.PrintGLError("CreateShaders useprogram");

            projectionMatrixLocation = GL.GetUniformLocation(shaderProgramHandle, "projectionMatrix");
            modelviewMatrixLocation = GL.GetUniformLocation(shaderProgramHandle, "modelViewMatrix");
            colorVec3Location = GL.GetUniformLocation(shaderProgramHandle, "inColor");

            float aspectRatio = Display.Instance.ClientSize.Width / (float)(Display.Instance.ClientSize.Height);
            //float aspectRatio = 1920.0f / 1080.0f;
            Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4f, aspectRatio, 1f, 100, out projectionMatrix);
            //Matrix4.CreateOrthographic(5, 5, 0.1f, 10000, out projectionMatrix);
            //Matrix4.CreateOrthographicOffCenter(-10, 10, -10, 10, 0.1f, 1000, out projectionMatrix);
            //projectionMatrix = Matrix4.Mult(Matrix4.LookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0)), projectionMatrix);
            modelviewMatrix = Matrix4.LookAt(new Vector3(3, 2, 5), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            //modelviewMatrix = Matrix4.Identity;
            //modelviewMatrix = Matrix4.CreateTranslation(0, 0, -50);

            GL.UniformMatrix4(projectionMatrixLocation, false, ref projectionMatrix);
            GL.UniformMatrix4(modelviewMatrixLocation, false, ref modelviewMatrix);
        }

        public static void Load(object sender, EventArgs e)
        {
           
            Util.PrintGLError("OnLoad");
            GenerateVBOs();
            Util.PrintGLError("GenerateVBOs");
            CreateShaders();
            Util.PrintGLError("CreateShaders");
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
