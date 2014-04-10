using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Explicatio.Controls;
using OpenTK.Input;

namespace Explicatio.Graphics
{
    public static class Camera
    {
        private static float zoom = 1.0f;
        private static float posX = 0;
        private static float posY = -1.877697f;
        private static int BORDERSIZE = 50;
        private static float STEPSPEED = 1f;

        //!? Properties region
        #region PROPERTIES
        public static float Zoom
        {
            get { return Camera.zoom; }
            set { Camera.zoom = value; }
        }
        public static float PosX
        {
            get { return Camera.posX; }
            set { Camera.posX = value; }
        }
        public static float PosY
        {
            get { return Camera.posY; }
            set { Camera.posY = value; }
        }
        #endregion
        //!? END of properties region
        public static float width;
        public static float height;
        public static void Update()
        {
            cameraMove();
            width = (((float)Display.Instance.ClientSize.Width / 40f)/zoom);
            height = (((float)Display.Instance.ClientSize.Height / 40f) / zoom);
            RenderingManager.ProjectionMatrix = Matrix4.CreateOrthographicOffCenter(-width - posX, width - posX, -height - posY, height - posY, 0.1f, 10000);
            RenderingManager.ProjectionMatrix = Matrix4.Mult(RenderingManager.ProjectionMatrix, Matrix4.CreateTranslation(0, 0, 1));
            RenderingManager.UpdateMatrices();
        }

        private static void cameraMove()
        {
            if (MyMouse.ButtonMiddle.IsToggled)
            {
                MyMouse.MouseDragPositionX = MyMouse.X;
                MyMouse.MouseDragPositionY = MyMouse.Y;
            }
            if (!MyMouse.ButtonMiddle.IsPressed)
            {
                //Keyboard move and zoom
                if (MyKeyboard.KeyMoveCameraLeft.IsPressed)
                {
                    Camera.PosX += STEPSPEED * (float)Math.Pow(Camera.Zoom, -1);
                }
                if (MyKeyboard.KeyMoveCameraRight.IsPressed)
                {
                    Camera.PosX -= STEPSPEED * (float)Math.Pow(Camera.Zoom, -1);
                }
                if (MyKeyboard.KeyMoveCameraDown.IsPressed)
                {
                    Camera.PosY += STEPSPEED * (float)Math.Pow(Camera.Zoom, -1);
                }
                if (MyKeyboard.KeyMoveCameraUp.IsPressed)
                {
                    Camera.PosY -= STEPSPEED * (float)Math.Pow(Camera.Zoom, -1);
                }
                if (MyKeyboard.KeyZoomDown.IsPressed)
                {
                    //Camera.Zoom += 0.05f * Camera.Zoom;
                    Camera.Zoom = 2f;
                }
                if (MyKeyboard.KeyZoomUp.IsPressed)
                {
                    //Camera.Zoom -= 0.05f * Camera.Zoom;
                        Camera.Zoom = 1f;
                }


                //TODO Gładkie przesuwanie (Im dalej na krańcu ekranu, tym szybciej)
                //Mouse move
                if (Display.Instance.WindowState == WindowState.Fullscreen)
                {
                    if (MyMouse.ChceckMouseRectangle(0, 0, Display.Instance.Height, Display.Instance.Height / Display.Instance.Height * BORDERSIZE))
                    {
                        Camera.PosY -= STEPSPEED / Camera.Zoom;
                    }
                    if (MyMouse.ChceckMouseRectangle(0, Display.Instance.Height - Display.Instance.Height / Display.Instance.Height * BORDERSIZE, Display.Instance.Width, Display.Instance.Height))
                    {
                        Camera.PosY += STEPSPEED / Camera.Zoom;
                    }
                    if (MyMouse.ChceckMouseRectangle(0, 0, Display.Instance.Width / Display.Instance.Width * BORDERSIZE, Display.Instance.Height))
                    {
                        Camera.PosX += STEPSPEED / Camera.Zoom;
                    }
                    if (MyMouse.ChceckMouseRectangle(Display.Instance.Width - Display.Instance.Width / Display.Instance.Width * BORDERSIZE, 0, Display.Instance.Width, Display.Instance.Height))
                    {
                        Camera.PosX -= STEPSPEED / Camera.Zoom;
                    }
                }

                //Scrool zoom
                Camera.Zoom += (MyMouse.WheelDelta / 8) * Camera.Zoom;
            }
            else
            {
                //Middle button move
                Camera.PosX += (float)(MyMouse.MouseDragPositionX - MyMouse.X) / 200 * (float)Math.Pow(Camera.Zoom, -1);
                Camera.PosY -= (float)(MyMouse.MouseDragPositionY - MyMouse.Y) / 200 * (float)Math.Pow(Camera.Zoom, -1);
            }


        }
    }
}
