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

        static private Matrix transform = Matrix.Identity;
        /// <summary>
        /// Matrix do wykonywania obliczeń
        /// </summary>
        public static Matrix Transform
        {
            get { return Camera.transform; }
            set { Camera.transform = value; }
        }
        static private float zoom = 0.01f;
        /// <summary>
        /// Zoom kamery
        /// </summary>
        static public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }
        static private float x = 50000f;
        /// <summary>
        /// Pozycja X kamrry
        /// </summary>
        public static float X
        {
            get { return Camera.x; }
            set { Camera.x = value; }
        }
        static private float y = 5000f;
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

        static public void Interaction(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicDevice)
        {
            if (MyMouse.ToogleMiddleButton() == false)
            {
                MyMouse.ScrollWheelMoveUpdate();
                Camera.Zoom += 0.25f * Camera.Zoom * (-MyMouse.ScrollWheelDelta / 120);
                //Camera.Zoom = Math.Min(4.5f, Math.Max(0.01f, (float)Math.Round(Camera.Zoom, 1)));
                //Camera.Zoom += 0.2f * (-MyMouse.ScrollWheelDelta / 120);
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
