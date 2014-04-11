using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OpenTK;
using OpenTK.Input;
using Explicatio.Graphics;
using System.Drawing;
using Explicatio.Worlds;
using Explicatio.Main;

namespace Explicatio.Controls
{
    public static class MyMouse
    {
        private static int x;
        private static int y;
        private static int xDelta;
        private static int yDelta;
        private static float wheel;
        private static float wheelDelta;
        private static int mouseDragPositionX;
        private static int mouseDragPositionY;
        private static float xRelative;
        private static float yRelative;

        #region Buttons
        private static MyKey buttonMiddle = new MyKey("Middle button??", MouseButton.Middle);

        public static MyKey ButtonMiddle
        {
            get { return MyMouse.buttonMiddle; }
            set { MyMouse.buttonMiddle = value; }
        }
        #endregion

        //!? Properties region
        #region PROPERTIES
        public static int X
        {
            get { return MyMouse.x; }
            set { MyMouse.x = value; }
        }
        public static int Y
        {
            get { return MyMouse.y; }
            set { MyMouse.y = value; }
        }
        public static float Wheel
        {
            get { return MyMouse.wheel; }
            set { MyMouse.wheel = value; }
        }
        public static float WheelDelta
        {
            get { return MyMouse.wheelDelta; }
            set { MyMouse.wheelDelta = value; }
        }
        public static int XDelta
        {
            get { return MyMouse.xDelta; }
            set { MyMouse.xDelta = value; }
        }
        public static int YDelta
        {
            get { return MyMouse.yDelta; }
            set { MyMouse.yDelta = value; }
        }
        public static int MouseDragPositionX
        {
            get { return MyMouse.mouseDragPositionX; }
            set { MyMouse.mouseDragPositionX = value; }
        }
        public static int MouseDragPositionY
        {
            get { return MyMouse.mouseDragPositionY; }
            set { MyMouse.mouseDragPositionY = value; }
        }
        /// <summary>
        /// Pozycja relatywna względem górnego czubka mapy
        /// </summary>
        public static float XRelative
        {
            get { return MyMouse.xRelative; }
            set { MyMouse.xRelative = value; }
        }
        /// <summary>
        /// Pozycja relatywna względem górnego czubka mapy
        /// </summary>
        public static float YRelative
        {
            get { return MyMouse.yRelative; }
            set { MyMouse.yRelative = value; }
        }
        #endregion
        //!? END of properties region

        public static void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
            x = e.X;
            y = e.Y;
            xDelta = e.XDelta;
            yDelta = e.YDelta;
        }
        public static void Mouse_WheelChanged(object sender, MouseWheelEventArgs e)
        {
            wheel = e.ValuePrecise;
            wheelDelta = e.DeltaPrecise;
        }


        public static void EndStep()
        {
            xRelative = -(Camera.PosX - ((MyMouse.X - Display.Instance.ClientSize.Width / 2f) / Camera.Zoom / 1.251167f))-32;
            yRelative = (Camera.PosY + ((MyMouse.Y - Display.Instance.ClientSize.Height / 2f) / Camera.Zoom / 1.251167f)) + (GameMain.CurrentWorld.Size * Chunk.CHUNK_SIZE) / 2; ;
            //Reset wheele delta
            wheelDelta = 0;
            //Reset mouse pos delta
            xDelta = 0;
            yDelta = 0;
   
        }

        public static bool ChceckMouseRectangle(int x1, int y1, int x2, int y2)
        {
            if (X >= x1 && X <= x2 && Y >= y1 && Y <= y2)
            {
                return true;
            }
            return false;
        }

    }
}
