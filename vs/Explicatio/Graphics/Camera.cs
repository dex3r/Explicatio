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
        private static float zoom = 10.0f;
        private static float posX = 0; //0->4
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
            get { return Camera.posX; }
            set { Camera.posX = value; ; }
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
            width = (((float)Display.Instance.ClientSize.Width) / zoom);
            height = (((float)Display.Instance.ClientSize.Height) / zoom);
            RenderingManager.ProjectionMatrix = Matrix4.CreateOrthographicOffCenter(-width - posX, width - posX, -height - posY, height - posY, -1f, 1f);
            RenderingManager.ProjectionMatrix = Matrix4.Mult(RenderingManager.ProjectionMatrix, Matrix4.CreateTranslation(0, 0, 1));

            RenderingManager.UpdateMatrices();
        }

        private static void cameraMove()
        {
            float scrollSpeed = STEPSPEED / (Camera.Zoom / 15);
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
                    Camera.posX += scrollSpeed;
                }
                if (MyKeyboard.KeyMoveCameraRight.IsPressed)
                {
                    Camera.posX -= scrollSpeed;
                }
                if (MyKeyboard.KeyMoveCameraDown.IsPressed)
                {
                    Camera.posY += scrollSpeed;
                }
                if (MyKeyboard.KeyMoveCameraUp.IsPressed)
                {
                    Camera.posY -= scrollSpeed;
                }
                if (MyKeyboard.KeyZoomDown.IsToggled)
                {
                    //Camera.Zoom += 0.05f * Camera.Zoom;
                    Camera.Zoom += ZOOMSPEED;
                }
                if (MyKeyboard.KeyZoomUp.IsToggled)
                {
                    //Camera.Zoom -= 0.05f * Camera.Zoom;
                    Camera.Zoom -= ZOOMSPEED;
                }


                //TODO Gładkie przesuwanie (Im dalej na krańcu ekranu, tym szybciej)
                //Mouse move
                if (Display.Instance.WindowState == WindowState.Fullscreen)
                {
                    if (MyMouse.ChceckMouseRectangle(0, 0, Display.Instance.Height, Display.Instance.Height / Display.Instance.Height * BORDERSIZE))
                    {
                        Camera.posY -= scrollSpeed;
                    }
                    if (MyMouse.ChceckMouseRectangle(0, Display.Instance.Height - Display.Instance.Height / Display.Instance.Height * BORDERSIZE, Display.Instance.Width, Display.Instance.Height))
                    {
                        Camera.posY += scrollSpeed;
                    }
                    if (MyMouse.ChceckMouseRectangle(0, 0, Display.Instance.Width / Display.Instance.Width * BORDERSIZE, Display.Instance.Height))
                    {
                        Camera.posX += scrollSpeed;
                    }
                    if (MyMouse.ChceckMouseRectangle(Display.Instance.Width - Display.Instance.Width / Display.Instance.Width * BORDERSIZE, 0, Display.Instance.Width, Display.Instance.Height))
                    {
                        Camera.posX -= scrollSpeed;
                    }
                }

                //Scrool zoom
                Camera.Zoom += (MyMouse.WheelDelta / 8) * zoom;
            }
            else
            {
                //Middle button move
                Camera.posX += (float)(MyMouse.MouseDragPositionX - MyMouse.X) / (Camera.Zoom * 40) * Camera.Zoom;
                Camera.posY -= (float)(MyMouse.MouseDragPositionY - MyMouse.Y) / (Camera.Zoom * 40) * Camera.Zoom;
            }


        }
    }
}
