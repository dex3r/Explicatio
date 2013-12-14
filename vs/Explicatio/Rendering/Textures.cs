using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Explicatio.Rendering
{
    public static class Textures
    {
        public static Texture2D Grass;
        public static Texture2D Snow;

        public static void Load(ContentManager cm)
        {
            Grass = cm.Load<Texture2D>(@"gfx\terrain\1");
            Snow = cm.Load<Texture2D>(@"gfx\terrain\2");
        }
    }
}
