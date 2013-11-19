using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Explicatio.Rendering
{
    public class GlobalRenderer : Renderer
    {
        public void Draw(SpriteBatch batch, GameTime time)
        {
                 for (int x = 0; x < 8; x++)
                     for (int y = 0; y < 8; y++)
                     {
                         batch.Draw(Textures.Grass, new Vector2(x * 64, y * 32), Color.White);
                         batch.Draw(Textures.Grass, new Vector2(x * 64 + 32, y * 32 + 16), Color.White);
                     }
        }
    }
}
