using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Explicatio.Rendering
{
    public interface Renderer
    {
        void Draw(SpriteBatch batch, GameTime time);
    }
}
