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
using Explicatio.Rendering;
using Explicatio.Controls;
using Explicatio.Worlds;

namespace Explicatio.Main
{
    public static class GameMain
    {
        private static bool isClosing;
        private static bool wasUpdated;
        private static World currentWorld;

        //!? Properties region
        #region PROPERTIES
        public static bool IsClosing
        {
            get { return GameMain.isClosing; }
        }
        public static World CurrentWorld
        {
            get { return GameMain.currentWorld; }
            set { GameMain.currentWorld = value; }
        }
        #endregion
        //!? END of properties region

        public static void Load(object sender, EventArgs e)
        {
            currentWorld = new World(512);

            //GL.Enable(EnableCap.Blend);
            //GL.BlendColor(1.0f, 0.0f, 1.0f, 0.0f);
            //GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            //GL.BlendFunc(BlendingFactorSrc.ConstantColor, BlendingFactorDest.OneMinusConstantAlpha);

            Shader.Init();
            Util.PrintGLError("Shaders init");
            GL.UseProgram(0);
            RenderingManager.Init();
            Util.PrintGLError("Renderer init");
            Primitive.InitPrimitives();
            Util.PrintGLError("Primitives init");
            GlobalRenderer.InitTemp();
            Util.PrintGLError("GlobalRenderer InitTemp");

            GL.FrontFace(FrontFaceDirection.Cw);
            GL.Enable(EnableCap.CullFace);
        }

        public static void Update(object sender, FrameEventArgs e)
        {
            MyKeyboard.Update();
            Camera.Update();
            DebugConsole.Show();
            wasUpdated = true;
            if (MyKeyboard.KeyExitGame.IsPressed)
            {
                Display.Instance.Exit();
            }
            if (MyKeyboard.KeyFullscreen.IsToggled)
            {
                Display.FullScreenSwitch();
            }
            MyMouse.EndStep();
            wasUpdated = true;
        }

        public static void Draw(object sender, FrameEventArgs e)
        {
            if (!wasUpdated)
            {
                return;
            }
            GL.Clear(ClearBufferMask.ColorBufferBit);

            RenderingManager.ModelMatrix = Matrix4.Identity;

            //Primitive.singleColorQuad.Color = new Vector3(0.0f, 1.0f, 0.0f);
            //RenderingManager.ModelMatrix = Matrix4.CreateScale(4f, 4f, 1f) * Matrix4.CreateTranslation(-3f, 0f, 0f);
            //Primitive.singleColorQuad.Draw();

            //RenderingManager.ModelMatrix = Matrix4.Identity;
            //Primitive.singleColorTriangle.Color = new Vector3(1.0f, 0.0f, 0.0f);
            //Primitive.singleColorTriangle.Draw();

            //Primitive.singleColorTriangle.Color = new Vector3(1.0f, 0, 0);
            //RenderingManager.ModelMatrix = Matrix4.CreateTranslation(2f, 0, 0);
            //Primitive.singleColorTriangle.Draw();

            GlobalRenderer.RenderAllChunks();
            //RenderingManager.ChangeCurrentShader(Shader.SimpleColorShader, false);

            
            Primitive.singleColorQuad.Color = new Vector3(1.0f, 0.0f, 0.0f);
            RenderingManager.ModelMatrix = Matrix4.Identity;
            Primitive.singleColorQuad.Draw();
            Primitive.singleColorQuad.Color = new Vector3(0.0f, 1.0f, 0.0f);
            RenderingManager.ModelMatrix = Matrix4.CreateTranslation(-2, 0, 0);
            Primitive.singleColorQuad.Draw();

            Util.PrintGLError("Render");

            Display.Instance.SwapBuffers();
            wasUpdated = false;
        }

        public static void Main(String[] args)
        {
            //Console.ReadKey();
            wasUpdated = false;
            using (Display display = new Display())
            {
                display.Run();
                isClosing = true;
            }
        }
    }
}
