using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Explicatio.Rendering
{
    public sealed class GlobalRenderer : Renderer
    {
        public GlobalRenderer(SpriteBatch batch)
            : base(batch)
        {

        }

        public override void Draw(GameTime time)
        {
            batch.Draw(Textures.Block, new Vector2(240, 20), Color.White);
            batch.Draw(Textures.Block, new Vector2(240 + 64, 20), Color.White);
            batch.Draw(Textures.Block, new Vector2(240, 20 + 32), Color.White);
            batch.Draw(Textures.Block, new Vector2(240 + 32, 20 + 16), Color.White);
            batch.Draw(Textures.Block, new Vector2(240 + 64, 20 + 32), Color.White);
            batch.Draw(Textures.Block, new Vector2(240 + 32 + 64, 20 + 16), Color.White);
        }
    }
}
