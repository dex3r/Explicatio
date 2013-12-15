using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using Explicatio.Controls;
using Explicatio.Main;

namespace Explicatio.Rendering
{
    public static class Camera
    {
        private static readonly float[] zoomSteps = {0.2f, 0.4f, 0.6f, 0.8f, 1f, 1.2f, 1.4f, 1.8f, 2.2f, 2.8f, 3.2f };
        public static float[] ZoomSteps
        {
            get { return Camera.zoomSteps; }
        }

        private static int currentZoomStepNumber = 1;
        public static int CurrentZoomStepNumber
        {
            get { return Camera.currentZoomStepNumber; }
            set { Camera.currentZoomStepNumber = value; }
        }

        public static float CurrentZoomStep
        {
            get { return zoomSteps[currentZoomStepNumber]; }
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
        static private float x;
        /// <summary>
        /// Pozycja X kamrry
        /// </summary>
        public static float X
        {
            get { return Camera.x; }
            set { Camera.x = value; }
        }
        static private float y;
        /// <summary>
        /// Pozycja Y kamery
        /// </summary>
        public static float Y
        {
            get { return Camera.y; }
            set { Camera.y = value; }
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
        /// Aktualizacja kamery
        /// </summary>
        static public void Update(GraphicsDevice graphicDevice)
        {
            Transform = Matrix.CreateTranslation(-(graphicDevice.Viewport.Width / 2 + X), -(graphicDevice.Viewport.Height / 2 + Y), 0) *
                       Matrix.CreateScale(zoom) *
                       Matrix.CreateTranslation(graphicDevice.Viewport.Width / 2, graphicDevice.Viewport.Height / 2, 0);
        }

        public static Matrix CreateVirtualTransofrmation(float virtualZoom)
        {
            return Matrix.CreateTranslation(-(GameMain.SpriteBatch.GraphicsDevice.Viewport.Width / 2 + X), -(GameMain.SpriteBatch.GraphicsDevice.Viewport.Height / 2 + Y), 0) *
                       Matrix.CreateScale(virtualZoom) *
                       Matrix.CreateTranslation(GameMain.SpriteBatch.GraphicsDevice.Viewport.Width / 2, GameMain.SpriteBatch.GraphicsDevice.Viewport.Height / 2, 0);
        }

        /// <summary>
        /// Przesuwanie kamery na wszystkei sposoby
        /// </summary>
        static public void Interaction(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicDevice)
        {
            if (MyMouse.ToogleMiddleButton() == false)
            {
                int steps = -MyMouse.ScrollWheelDelta / 120;
                currentZoomStepNumber += steps;
                if (currentZoomStepNumber < 0)
                {
                    currentZoomStepNumber = 0;
                }
                else if (currentZoomStepNumber >= zoomSteps.Length)
                {
                    currentZoomStepNumber = zoomSteps.Length - 1;
                }
                zoom = zoomSteps[currentZoomStepNumber];

                if (graphicsDeviceManager.IsFullScreen == true)
                {
                    if (MyMouse.ChceckMouseRectangle(0, 0, graphicDevice.Viewport.Width, graphicDevice.Viewport.Height / graphicDevice.Viewport.Height * BORDERSIZE)) Camera.Y -= STEP / Camera.Zoom;
                    if (MyMouse.ChceckMouseRectangle(0, graphicDevice.Viewport.Height - graphicDevice.Viewport.Height / graphicDevice.Viewport.Height * BORDERSIZE, graphicDevice.Viewport.Width, graphicDevice.Viewport.Height)) Camera.Y += STEP / Camera.Zoom;
                    if (MyMouse.ChceckMouseRectangle(0, 0, graphicDevice.Viewport.Width / graphicDevice.Viewport.Width * BORDERSIZE, graphicDevice.Viewport.Height)) Camera.X -= STEP / Camera.Zoom;
                    if (MyMouse.ChceckMouseRectangle(graphicDevice.Viewport.Width - graphicDevice.Viewport.Width / graphicDevice.Viewport.Width * BORDERSIZE, 0, graphicDevice.Viewport.Width, graphicDevice.Viewport.Height)) Camera.X += STEP / Camera.Zoom;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left)) Camera.X -= STEP / Camera.Zoom;
                if (Keyboard.GetState().IsKeyDown(Keys.Right)) Camera.X += STEP / Camera.Zoom;
                if (Keyboard.GetState().IsKeyDown(Keys.Up)) Camera.Y -= STEP / Camera.Zoom;
                if (Keyboard.GetState().IsKeyDown(Keys.Down)) Camera.Y += STEP / Camera.Zoom;
            }
            else
            {
                Camera.X -= (MyMouse.MouseHoldPositionX - Mouse.GetState().X) / 40 / Camera.Zoom;
                Camera.Y -= (MyMouse.MouseHoldPositionY - Mouse.GetState().Y) / 40 / Camera.Zoom;
            }
        }
    }
}
