using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Explicatio
{
    public static class GameWindowControl
    {

        /// <summary>
        ///  http://projectdrake.net/blog/2013/03/31/tutorial-setting-window-position-in-xnamonogame/
        /// </summary>
        public static void SetPosition(this GameWindow window, Point position)
        {
            OpenTK.GameWindow OTKWindow = GetForm(window);
            if (OTKWindow != null)
            {
                OTKWindow.X = position.X;
                OTKWindow.Y = position.Y;
            }
        }

        public static OpenTK.GameWindow GetForm(this GameWindow gameWindow)
        {
            Type type = typeof(OpenTKGameWindow);
            System.Reflection.FieldInfo field = type.GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
                return field.GetValue(gameWindow) as OpenTK.GameWindow;
            return null;
        }
        //MÓJ KOD
        public static Vector2 GetPosition(this GameWindow window)
        {
            OpenTK.GameWindow OTKWindow = GetForm(window);
            return new Vector2();
        }

    }
}
