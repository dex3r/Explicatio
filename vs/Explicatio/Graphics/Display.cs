using Explicatio.Controls;
using Explicatio.Main;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Graphics
{
    public class Display : GameWindow
    {
        private static Display instance;

        //!? Properties region
        #region PROPERTIES
        public static Display Instance
        {
            get { return Display.instance; }
        }
        #endregion
        //!? END of properties region

        public Display()
            : base(800, 600, GraphicsMode.Default, "Explicatio INDEV", GameWindowFlags.Default, DisplayDevice.Default, 1, 0, GraphicsContextFlags.Debug, null)
        {
            instance = this;
            base.Load += GameMain.Load;
            base.UpdateFrame += GameMain.Update;
            base.RenderFrame += GameMain.Draw;
            //this.Mouse.Move += MyMouse.Mouse_Move;
            this.Mouse.WheelChanged += MyMouse.Mouse_WheelChanged;
            //this.Mouse.ButtonDown += MyMouse.Mouse_ButtonDown;
            //this.Mouse.ButtonUp += MyMouse.Mouse_ButtonUp;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(this.ClientRectangle);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, -1, 0);
        }

        protected override void OnLoad(EventArgs e)
        {
            WindowInit();
            GL.ClearColor(Color.CornflowerBlue);
            GL.Viewport(0, 0, Width, Height);
            VSync = VSyncMode.On;
            base.OnLoad(e);
        }
       

        public void WindowInit()
        {
            this.Size = new System.Drawing.Size(800, 600);
            WindowBorder = WindowBorder.Resizable;
        }

        public static void FullScreenSwitch()
        {
            if (Display.Instance.WindowState != WindowState.Fullscreen)
            {
                Display.Instance.WindowBorder = WindowBorder.Hidden;
                Display.Instance.WindowState = WindowState.Fullscreen;
            }
            else
            {
                Display.Instance.WindowBorder = WindowBorder.Fixed;
                Display.Instance.WindowState = WindowState.Normal;
            }
        }
    }
}
