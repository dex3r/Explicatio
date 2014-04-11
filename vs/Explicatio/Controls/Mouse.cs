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
using Explicatio.Utils;

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
        private static float xClient;


        private static float yClient;


        private static float xWorld;


        private static float yWorld;



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
        /// Pozycja X myszki względem clienta OpenGL i aktualnej transformacji
        /// </summary>
        public static float XClient
        {
            get { return MyMouse.xClient; }
        }
        /// <summary>
        /// Pozycja Y myszki względem clienta OpenGL i aktualnej transformacji
        /// </summary>
        public static float YClient
        {
            get { return MyMouse.yClient; }
        }
        /// <summary>
        /// Pozycja X myszki względem świata wyrażona w blokach
        /// </summary>
        public static float XWorld
        {
            get { return MyMouse.xWorld; }
        }
        /// <summary>
        /// Pozycja Y myszki względem świata wyrażona w blokach
        /// </summary>
        public static float YWorld
        {
            get { return MyMouse.yWorld; }
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
            y = Display.Instance.Mouse.Y;
            x = Display.Instance.Mouse.X;
            //xClient = -(Camera.PosX - ((MyMouse.X - Display.Instance.ClientSize.Width / 2f) / Camera.Zoom / 1.251167f))-32;
            //yClient = (Camera.PosY + ((MyMouse.Y - Display.Instance.ClientSize.Height / 2f) / Camera.Zoom / 1.251167f)) + (GameMain.CurrentWorld.Size * Chunk.CHUNK_SIZE) / 2;
            Vector2? v = ScreenPointToClient(x, y);
            xClient = v.Value.X;
            yClient = v.Value.Y;
            v = ClientPointToWorld(xClient, yClient);
            if(v.HasValue)
            {
                xWorld = v.Value.X;
                yWorld = v.Value.Y;
            }
            //Reset wheele delta
            wheelDelta = 0;
            //Reset mouse pos delta
            xDelta = 0;
            yDelta = 0;
        }

        /// <summary>
        /// Oblicz pozycję w przestrzeni klienta (OpenGL) z punktu na ekranie
        /// </summary>
        /// <param name="screenX"></param>
        /// <param name="screenY"></param>
        /// <returns></returns>
        public static Vector2 ScreenPointToClient(float screenX, float screenY)
        {
            return new Vector2(
                (Camera.PosX / 2) - ((MyMouse.X - Display.Instance.ClientSize.Width / 2f) / Camera.Zoom),
                (Camera.PosY / 2) + ((MyMouse.Y - Display.Instance.ClientSize.Height / 2f) / Camera.Zoom) );
        }

        /// <summary>
        /// Zwraca null jeżeli currentWorld jest pusty, w przeciwnym wypadku pozycję w świecie podaną w blokach (x i y bloku)
        /// </summary>
        /// <returns></returns>
        public static Vector2? ClientPointToWorld(float clientX, float clientY)
        {
            if(GameMain.CurrentWorld == null)
            {
                return null;
            }
            // Zjebany, dla textur (n, 2n - 1) a konkretniej (64, 31)
            //return new Vector2(clientX / 2, ((clientY / 32) < clie);
            // Prawidłowy, dla textur (n, 2n):
            //DisplayString.Append("Block: ");
            //float mapX = (((clientX) + (clientY * 2f)) / 2f) + 0.5f;
            float mapX = (clientX / 2f) + clientY + 0.5f;
            float mapY = (clientX / 2f) - clientY + 0.5f;
            return new Vector2(mapX, mapY);
        }

        public static bool ChceckMouseRectangle(int x, int y, int width, int height)
        {
            return Util.IntersectPointRectangle(X, Y, x, y, width, height);
        }

    }
}
