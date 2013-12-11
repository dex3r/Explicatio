using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using Explicatio.Controls;

namespace Explicatio.Rendering
{
    public static class Camera
    {
#if DEBUG
        private static float[] zoomSteps = {0.02f, 0.05f, 0.1f, 0.2f, 0.4f, 0.6f, 0.8f, 1f, 1.2f, 1.4f, 1.8f, 2.2f, 2.8f, 3.2f };
#else
        private static float[] zoomSteps = { 0.2f, 0.4f, 0.6f, 0.8f, 1f, 1.2f, 1.4f, 1.8f, 2.2f, 2.8f, 3.2f };
#endif
        public static float[] ZoomSteps
        {
            get { return Camera.zoomSteps; }
            set { Camera.zoomSteps = value; }
        }

        private static int currentZoomStep = 1;
        public static int CurrentZoomStep
        {
            get { return Camera.currentZoomStep; }
            set { Camera.currentZoomStep = value; }
        }

        static private Matrix transform = Matrix.Identity;
        /// <summary>
        /// Matrix do wykonywania obliczeń
        /// </summary>
        public static Matrix Transform
        {
            get { return Camera.transform; }
            set { Camera.transform = value; }
        }
        static private float zoom = 1f;
        /// <summary>
        /// Zoom kamery
        /// </summary>
        static public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }
        static private float x = 0f;
        /// <summary>
        /// Pozycja X kamrry
        /// </summary>
        public static float X
        {
            get { return Camera.x; }
            set { Camera.x = value; }
        }
        static private float y = 0f;
        /// <summary>
        /// Pozycja Y kamery
        /// </summary>
        public static float Y
        {
            get { return Camera.y; }
            set { Camera.y = value; }
        }
        /// <summary>
        /// Aktualizacja kamery
        /// </summary>
        static public void Update(GraphicsDevice graphicDevice)
        {
            Transform = Matrix.CreateTranslation(-(graphicDevice.Viewport.Width / 2 + X), -(graphicDevice.Viewport.Height / 2 + Y), 0) *
                       Matrix.CreateScale(zoom) *
                       Matrix.CreateTranslation(graphicDevice.Viewport.Width / 2, graphicDevice.Viewport.Height / 2, 0);
        }

        /// <summary>
        /// Szybkość przesuwania kamery
        /// </summary>
        private const int STEP = 7;
        /// <summary>
        /// Wielkość krawędzi do przesuwania ekranu
        /// </summary>
        private const int BORDERSIZE = 15;
        /// <summary>
        /// Przesuwanie kamery na wszystkei sposoby
        /// </summary>
        static public void Interaction(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicDevice)
        {
            if (MouseAbsolute.ToogleMiddleButton() == false)
            {
                MouseAbsolute.ScrollWheelMoveUpdate();
                int steps = -MouseAbsolute.ScrollWheelDelta / 120;
                currentZoomStep += steps;
                if (currentZoomStep < 0)
                {
                    currentZoomStep = 0;
                }
                else if (currentZoomStep >= zoomSteps.Length)
                {
                    currentZoomStep = zoomSteps.Length - 1;
                }
                zoom = zoomSteps[currentZoomStep];

                //double z = 0.2f * Camera.Zoom * (-MouseAbsolute.ScrollWheelDelta / 120);
                //if(MouseAbsolute.ScrollWheelDelta != 0 && z == 0)
                //{
                //     z = 0.2f;
                //}
                //z += Camera.zoom;
                //Camera.zoom = Math.Min(4.5f, Math.Max(0.01f, (float)Math.Round(z, 1)));
                ////Camera.Zoom += 0.2f * (-MyMouse.ScrollWheelDelta / 120);
                if (graphicsDeviceManager.IsFullScreen == true)
                {
                    if (MouseAbsolute.ChceckMouseRectangle(0, 0, graphicDevice.Viewport.Width, graphicDevice.Viewport.Height / graphicDevice.Viewport.Height * BORDERSIZE)) Camera.Y -= STEP / Camera.Zoom;
                    if (MouseAbsolute.ChceckMouseRectangle(0, graphicDevice.Viewport.Height - graphicDevice.Viewport.Height / graphicDevice.Viewport.Height * BORDERSIZE, graphicDevice.Viewport.Width, graphicDevice.Viewport.Height)) Camera.Y += STEP / Camera.Zoom;
                    if (MouseAbsolute.ChceckMouseRectangle(0, 0, graphicDevice.Viewport.Width / graphicDevice.Viewport.Width * BORDERSIZE, graphicDevice.Viewport.Height)) Camera.X -= STEP / Camera.Zoom;
                    if (MouseAbsolute.ChceckMouseRectangle(graphicDevice.Viewport.Width - graphicDevice.Viewport.Width / graphicDevice.Viewport.Width * BORDERSIZE, 0, graphicDevice.Viewport.Width, graphicDevice.Viewport.Height)) Camera.X += STEP / Camera.Zoom;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left)) Camera.X -= STEP / Camera.Zoom;
                if (Keyboard.GetState().IsKeyDown(Keys.Right)) Camera.X += STEP / Camera.Zoom;
                if (Keyboard.GetState().IsKeyDown(Keys.Up)) Camera.Y -= STEP / Camera.Zoom;
                if (Keyboard.GetState().IsKeyDown(Keys.Down)) Camera.Y += STEP / Camera.Zoom;
            }
            else
            {
                Camera.X -= (MouseAbsolute.MouseHoldPositionX - Mouse.GetState().X) / 40 / Camera.Zoom;
                Camera.Y -= (MouseAbsolute.MouseHoldPositionY - Mouse.GetState().Y) / 40 / Camera.Zoom;
            }
        }
    }
}
