using Explicatio.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Controls
{
    public static class MyMouse
    {
        private static bool middleButtonStatus = false;
        public static int MouseHoldPositionX { get; private set; }
        public static int MouseHoldPositionY { get; private set; }
        public static int OverallScrollWheelValue { get; private set; }
        /// <summary>
        /// Różnica obrotów kółkiem od ostatniego Update 
        /// Uwaga! Jeden obrót to 120
        /// </summary>
        public static int ScrollWheelDelta { get; private set; }

        private static Vector2 positionRelative;

        public static Vector2 PositionRelative
        {
            get { return MyMouse.positionRelative; }
            set { MyMouse.positionRelative = value; }
        }
        /// <summary>
        /// Update relatywnej pozycji myszy i kółka //!TEMP Tworzy śnieg po wciśnięciu LPM
        /// </summary>
        public static void Update(World world)
        {
            ScrollWheelDelta = OverallScrollWheelValue - Mouse.GetState().ScrollWheelValue;
            OverallScrollWheelValue = Mouse.GetState().ScrollWheelValue;
            positionRelative.X = Rendering.Camera.Transform.Translation.X * -1 * (float)Math.Pow(Rendering.Camera.Zoom, -1) + Mouse.GetState().X * (float)Math.Pow(Rendering.Camera.Zoom, -1);
            positionRelative.Y = Rendering.Camera.Transform.Translation.Y * -1 * (float)Math.Pow(Rendering.Camera.Zoom, -1) + Mouse.GetState().Y * (float)Math.Pow(Rendering.Camera.Zoom, -1);

            //if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            //{
            //    Interaction(world);
            //}
        }


        public static bool ChceckMouseRectangle(int x1, int y1, int x2, int y2)
        {
            if (Mouse.GetState().X >= x1 && Mouse.GetState().X <= x2 && Mouse.GetState().Y >= y1 && Mouse.GetState().Y <= y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Fukncja pomocnicza służy do przesuwania obrazu po przytrzymaniu środkowego przycisku myszy
        /// </summary>
        /// <returns>True - ON, False - OFF</returns>
        public static bool ToogleMiddleButton()
        {
            if (middleButtonStatus == false && Mouse.GetState().MiddleButton == ButtonState.Pressed)
            {
                MouseHoldPositionX = Mouse.GetState().X;
                MouseHoldPositionY = Mouse.GetState().Y;
                middleButtonStatus = true;
            }
            else if (middleButtonStatus == true && Mouse.GetState().MiddleButton == ButtonState.Released)
            {
                middleButtonStatus = false;
            }
            return middleButtonStatus;
        }
    }
}
