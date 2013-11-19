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
        public Matrix Transform { get; private set; }
        public Viewport View { get; private set; }

        public Camera(Viewport view)
        {
            Transform = Matrix.Identity;
            View = view;
        }
        public void Update(GameTime gameTime, float rotation, Vector2 position, float zoom)
        {
            Transform = Matrix.CreateTranslation(-position.X, -position.Y, 0) *
                        Matrix.CreateRotationZ(rotation) *
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                        Matrix.CreateTranslation(View.Width / 2, View.Height / 2, 0);
        }
    }
}
 