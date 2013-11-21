using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Explicatio.Utils
{
    /// <summary>
    /// Narzędzie słóżące do wyświetlania tekstu
    /// jak i również logu!
    /// </summary>
    public static class Text
    {
        private static SpriteBatch spriteBatch;
        private static ContentManager contentManager;
        /// <summary>Zmiana string do loga. Możliwość implementacji w każdej metodzie i klasie </summary>
        public static string Log;
        public static void Load(SpriteBatch batch, ContentManager cm)
        {
            spriteBatch = batch;
            contentManager = cm;
            Log = "";
        }
        public static SpriteFont getFont(string fontName)
        {
            return contentManager.Load<SpriteFont>("fonts/" + fontName);
        }
        public static void LoadDefaultFont()
        {
            SpriteFont font = getFont("Courier New");
        }
        /// <summary>
        /// Wyświetlanie tekstu na różne możliwości żeby wywołać wystarczy wpisać Text.Draw(string,newVector2...); w jakiejkolwiek metodzei draw
        /// </summary>
        public static void Draw(string textString, Vector2 position)
        {
            spriteBatch.DrawString(getFont("Courier New"), textString, position, Color.Black, 0, new Vector2(0,0), 1.0f, SpriteEffects.None, 0.5f);
        }
        public static void Draw(string textString, Vector2 position, Color textColor)
        {
            spriteBatch.DrawString(getFont("Courier New"), textString, position, textColor, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
        }
        public static void Draw(string textString, Vector2 position, Color textColor, float textSize)
        {
            spriteBatch.DrawString(getFont("Courier New"), textString, position, textColor, 0, new Vector2(0, 0), textSize, SpriteEffects.None, 0.5f);
        }
        public static void Draw(string textString, Vector2 position, Color textColor, float textSize, string fontName)
        {
            spriteBatch.DrawString(getFont(fontName), textString, position, textColor, 0, new Vector2(0, 0), textSize, SpriteEffects.None, 0.5f);
        }
    }
}
