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
            GL.UseProgram(0);
            RenderingManager.Init();
            Util.PrintGLError("Renderer init");
            Primitive.InitPrimitives();
            Util.PrintGLError("Primitives init");
<<<<<<< HEAD
            GlobalRenderer.InitTemp();
            Util.PrintGLError("GlobalRenderer InitTemp");
=======

            GlobalRenderer.InitTemp();
            Util.PrintGLError("InitTemp");
>>>>>>> parent of fa5322c... Merge branch 'OpenTK' of https://github.com/dex3r/Explicatio into OpenTK

            GL.FrontFace(FrontFaceDirection.Cw);
            //GL.Enable(EnableCap.CullFace);
            GL.Disable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Blend);
            //GL.BlendColor(1.0f, 0.0f, 1.0f, 1.0f);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        
        public static void Update(object sender, FrameEventArgs e)
        {
            MyKeyboard.Update();
            Camera.Update();
            //Console.Clear();
            //Console.WriteLine(MyMouse.XRelative + " " + MyMouse.YRelative);
            //Console.WriteLine(Camera.PosX + " " + Camera.PosY);
            //Console.WriteLine(Camera.width + " " + Camera.height);
            //Console.WriteLine(MyMouse.X + " " + MyMouse.Y);
            //Console.WriteLine(Camera.Zoom);
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
<<<<<<< HEAD

            Console.WriteLine(Display.Instance.RenderFrequency);
            wasUpdated = true;
=======
>>>>>>> parent of fa5322c... Merge branch 'OpenTK' of https://github.com/dex3r/Explicatio into OpenTK
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

            Primitive.singleColorTriangle.Color = new Vector3(1.0f, 0, 0);
            RenderingManager.ModelMatrix = Matrix4.CreateTranslation(2f, 0, 0);
            Primitive.singleColorTriangle.Draw();

<<<<<<< HEAD
            GlobalRenderer.RenderAllChunks();
=======
            GlobalRenderer.RenderChunk(null);
>>>>>>> parent of fa5322c... Merge branch 'OpenTK' of https://github.com/dex3r/Explicatio into OpenTK
            //RenderingManager.ChangeCurrentShader(Shader.SimpleColorShader, false);

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
