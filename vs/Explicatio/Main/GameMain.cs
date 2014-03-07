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
using OpenTK.Input;
using Explicatio.Graphics;
using Explicatio.Utils;
using Explicatio.Graphics.Shaders;
using Explicatio.Graphics.Primitives;

namespace Explicatio.Main
{
    public static class GameMain
    {
        private static bool wasUpdated;

        //!? Properties region
        #region PROPERTIES

        #endregion
        //!? END of properties region

        public static void Load(object sender, EventArgs e)
        {
            Shader.Init();
            Util.PrintGLError("Shaders init");
            Renderer.Init();
            Util.PrintGLError("Renderer init");
            Primitive.InitPrimitives();
            Util.PrintGLError("Primitives init");
            GL.Enable(EnableCap.DepthTest);
            GL.FrontFace(FrontFaceDirection.Cw);
        }

        static float modelX = 0, modelY = 0, modelZ = -30;
        public static void Update(object sender, FrameEventArgs e)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks[OpenTK.Input.Key.Left])
            {
                modelX++;
            }
            if (ks[OpenTK.Input.Key.Down])
            {
                modelY++;
                //modelZ -= 1 / 3f;
            }
            if (ks[OpenTK.Input.Key.Right])
            {
                modelX--;
            }
            if (ks[OpenTK.Input.Key.Up])
            {
                modelY--;
                //modelZ += 1 / 3f;
            }
            if (ks[OpenTK.Input.Key.Z])
            {
                modelZ++;
            }
            if (ks[OpenTK.Input.Key.X])
            {
                modelZ--;
            }
            //modelZ--;
            Renderer.ViewMatrix = Matrix4.CreateTranslation(modelX / 10f, modelY / 10f, modelZ / 10f);
            //Renderer.ModelMatrix = Matrix4.Identity;
            Renderer.CurrentShader.UpdateAllUniforms();
            //Matrix4 m = Matrix4.CreateTranslation(modelX, modelY, modelZ);
            //Renderer.ViewMatrix = Matrix4.Mult(Renderer.ViewMatrix, m);
            //Matrix4 m = Matrix4.Mult(Matrix4.CreateTranslation(modelX / 2f, modelY / 2f, modelZ / 2f), projectionMatrix);
            wasUpdated = true;
        }

        public static void Draw(object sender, FrameEventArgs e)
        {
            if (!wasUpdated)
            {
                return;
            }
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Renderer.ModelMatrix = Matrix4.Identity;

            Primitive.singleColorTriangle.Color = new Vector3(1.0f, 0.0f, 0.0f);
            Primitive.singleColorTriangle.Draw();

            Renderer.ModelMatrix = Matrix4.CreateTranslation(2f, 0, 0);
            Primitive.singleColorTriangle.Draw();

            Primitive.singleColorQuad.Color = new Vector3(0.0f, 1.0f, 0.0f);
            Renderer.ModelMatrix = Matrix4.CreateTranslation(-3f, 0, 0);
            Primitive.singleColorQuad.Draw();

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
