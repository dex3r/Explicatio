using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

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
        static public void UpdateCamera(GraphicsDevice graphicDevice)
        {
            Transform = Matrix.CreateTranslation(-(graphicDevice.Viewport.Width / 2 + X), -(graphicDevice.Viewport.Height / 2 + Y), 0) *
                       Matrix.CreateScale(zoom) *
                       Matrix.CreateTranslation(graphicDevice.Viewport.Width / 2, graphicDevice.Viewport.Height / 2, 0);
        }
    }
}
