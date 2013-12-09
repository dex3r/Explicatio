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
    public class Camera
    {
        internal Matrix Transform { get; private set; }
        internal Viewport View { get; private set; }

        private float zoom;
        public float Zoom
        {
            get { return zoom; }
            set { zoom = value;  }
        }
        public float X { get; set; }
        public float Y { get; set; }

        public Camera(Viewport view)
        {
            Transform = Matrix.Identity;
            View = view;
            zoom = 0.01f;
            X = 50000f;
            Y = 5000f;
        }
        public void UpdateCamera()
        {
             Transform = Matrix.CreateTranslation(-(View.Width / 2 + X), -(View.Height / 2 + Y), 0) *
                        Matrix.CreateScale(zoom) *
                        Matrix.CreateTranslation(View.Width / 2, View.Height / 2, 0);
        }
    }
}
 