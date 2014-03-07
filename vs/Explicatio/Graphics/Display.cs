using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Explicatio.Main;
using System.Drawing;

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
        {
            instance = this;
            base.Load += GameMain.Load;
            base.UpdateFrame += GameMain.Update;
            base.RenderFrame += GameMain.Draw;
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Size = new System.Drawing.Size(800, 600);
            GL.ClearColor(Color.CornflowerBlue);
            GL.Viewport(0, 0, Width, Height);
            VSync = VSyncMode.On;
            base.OnLoad(e);
        }
    }
}
