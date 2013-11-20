using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Explicatio.Main
{
    public static class Utils
    {
        public static Rectangle RotateRectangle()
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// NIE DZIAŁA JESZCZE!!
    /// </summary>
    public class Text
    {
        private static string textString;
        public string TextString
        {
            get { return textString; }
            set { textString = value;}
        }
        public Vector2 Postion { get; private set; }
        private static SpriteFont font;
        public SpriteFont Font 
        {
            get { return font; }
            set { font = value;}
        }
        public Color TextColor { get; private set;} 
        public Text(Vector2 position, ContentManager cm)
        {
            textString = "";
            Postion = position;
            Font = null;
            //Font = cm.Load<SpriteFont>("fonts/Courier New");
            TextColor = Color.Black;
        }
        public void Draw(SpriteBatch batch, GameTime time)
        {
            batch.DrawString(Font, TextString, Postion, TextColor,
        0, Font.MeasureString(TextString) / 2, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
