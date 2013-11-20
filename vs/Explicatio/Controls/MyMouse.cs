using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Explicatio.Controls
{
    public static class MyMouse
    {
        private static MouseState mouseState = Mouse.GetState();
        public static int OverallScrollWheelValue { get; private set; }
        /// <summary>
        /// Różnica obrotów kółkiem od ostatniego Update 
        /// Uwaga! Jeden obrót to 120
        /// </summary>
        public static int ScrollWheelDelta { get; private set; }

        public static void Update()
        {
            ScrollWheelDelta = OverallScrollWheelValue - Mouse.GetState().ScrollWheelValue;
            OverallScrollWheelValue = Mouse.GetState().ScrollWheelValue;
        }

        public static bool ChceckMouse(Rectangle rect)
        {
            if (mouseState.X>=rect.X && mouseState.X <= rect.X+rect.Width && mouseState.Y >= rect.Y && mouseState.Y <= rect.Y+rect.Height) return true;
            return false;
        }
    }
}
