using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Explicatio.Blocks
{
    public class IMetaTexture
    {
        /// <summary>
        /// Zwraca teksturę w zależności do metadaty
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns>Tekstura w zależności do metadaty</returns>
        public Texture2D GetTextureByMetadata(byte metadata);
    }
}
