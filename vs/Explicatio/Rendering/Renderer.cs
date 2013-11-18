using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Explicatio.Rendering
{
    public abstract class Renderer
    {
        protected SpriteBatch batch;
        public SpriteBatch Batch
        {
            get { return batch; }
            set { batch = value; }
        }

        public Renderer(SpriteBatch batch)
        {
            this.batch = batch;
        }

        public abstract void Draw(GameTime time);
    }
}
