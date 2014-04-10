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
        private static float posX = -2; //0->4
        private static float posY = 0; //0->-1.9375
        private static readonly int BORDERSIZE = 50;
        private static readonly float STEPSPEED = 1f;
        private static readonly float ZOOMSPEED = 1f;

        //!? Properties region
        #region PROPERTIES
        public static float Zoom
        {
            get { return Camera.zoom; }
            set { Camera.zoom = value; }
        }
        public static float PosX
        {
            get { return Camera.posX / 0.0625f; }
            set { Camera.posX = value * 0.0625f; }
        }
        public static float PosY
        {
            get { return Camera.posY / 0.0625f; }
            set { Camera.posY = value * 0.0625f; }
        }
        #endregion
        //!? END of properties region
        public static float width;
        public static float height;
        public static void Update()
        {
            cameraMove();
            width = (((float)Display.Instance.ClientSize.Width) / 40f / zoom);
            height = (((float)Display.Instance.ClientSize.Height) / 40f / zoom);
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
                    Camera.posX += STEPSPEED / Camera.Zoom;
                }
                if (MyKeyboard.KeyMoveCameraRight.IsPressed)
                {
                    Camera.posX -= STEPSPEED / Camera.Zoom;
                }
                if (MyKeyboard.KeyMoveCameraDown.IsPressed)
                {
                    Camera.posY += STEPSPEED / Camera.Zoom;
                }
                if (MyKeyboard.KeyMoveCameraUp.IsPressed)
                {
                    Camera.posY -= STEPSPEED / Camera.Zoom;
                }
                if (MyKeyboard.KeyZoomDown.IsPressed)
                {
                    //Camera.Zoom += 0.05f * Camera.Zoom;
                    Camera.Zoom = ZOOMSPEED;
                }
                if (MyKeyboard.KeyZoomUp.IsPressed)
                {
                    //Camera.Zoom -= 0.05f * Camera.Zoom;
                    Camera.Zoom = ZOOMSPEED;
                }


                //TODO Gładkie przesuwanie (Im dalej na krańcu ekranu, tym szybciej)
                //Mouse move
                if (Display.Instance.WindowState == WindowState.Fullscreen)
                {
                    if (MyMouse.ChceckMouseRectangle(0, 0, Display.Instance.Height, Display.Instance.Height / Display.Instance.Height * BORDERSIZE))
                    {
                        Camera.posY -= STEPSPEED / Camera.Zoom;
                    }
                    if (MyMouse.ChceckMouseRectangle(0, Display.Instance.Height - Display.Instance.Height / Display.Instance.Height * BORDERSIZE, Display.Instance.Width, Display.Instance.Height))
                    {
                        Camera.posY += STEPSPEED / Camera.Zoom;
                    }
                    if (MyMouse.ChceckMouseRectangle(0, 0, Display.Instance.Width / Display.Instance.Width * BORDERSIZE, Display.Instance.Height))
                    {
                        Camera.posX += STEPSPEED / Camera.Zoom;
                    }
                    if (MyMouse.ChceckMouseRectangle(Display.Instance.Width - Display.Instance.Width / Display.Instance.Width * BORDERSIZE, 0, Display.Instance.Width, Display.Instance.Height))
                    {
                        Camera.posX -= STEPSPEED / Camera.Zoom;
                    }
                }

                //Scrool zoom
                Camera.Zoom += (MyMouse.WheelDelta / 8) * Camera.Zoom;
            }
            else
            {
                //Middle button move
                Camera.posX += (float)(MyMouse.MouseDragPositionX - MyMouse.X) / (STEPSPEED * 200) / Camera.Zoom;
                Camera.posY -= (float)(MyMouse.MouseDragPositionY - MyMouse.Y) / (STEPSPEED * 200) / Camera.Zoom;
            }


        }
    }
}
