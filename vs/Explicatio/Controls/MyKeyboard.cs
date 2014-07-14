using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace Explicatio.Controls
{
    public static class MyKeyboard
    {
        private static List<MyKey> allKeys = new List<MyKey>();
        private static KeyboardState keysState;
        private static MyKey keyExitGame = new MyKey("Close game window", Key.Escape);

        private static MyKey keyZoomUp = new MyKey("Zoom camera out", Key.Minus);
        private static MyKey keyZoomDown = new MyKey("Zoom camera in", Key.Plus);
        private static MyKey keyMoveCameraLeft = new MyKey("Move camera left", Key.Left);
        private static MyKey keyMoveCameraRight = new MyKey("Move camera right", Key.Right);
        private static MyKey keyMoveCameraUp = new MyKey("Move camera up", Key.Up);
        private static MyKey keyMoveCameraDown = new MyKey("Move camera down", Key.Down);

        private static MyKey keyResUp = new MyKey("Increase resolution", Key.F6);
        private static MyKey keyResDown = new MyKey("Decrease resolution", Key.F5);
        private static MyKey keyFullscreen = new MyKey("Switch full screen mode", Key.F4);
        public static readonly MyKey KeyPlaceBlock = new MyKey("Place block", MouseButton.Left);

        public static List<MyKey> AllKeys
        {
            get { return MyKeyboard.allKeys; }
            set { MyKeyboard.allKeys = value; }
        }
        public static MyKey KeyExitGame
        {
            get { return MyKeyboard.keyExitGame; }
            set { MyKeyboard.keyExitGame = value; }
        }
        public static MyKey KeyZoomUp
        {
            get { return MyKeyboard.keyZoomUp; }
            set { MyKeyboard.keyZoomUp = value; }
        }
        public static MyKey KeyZoomDown
        {
            get { return MyKeyboard.keyZoomDown; }
            set { MyKeyboard.keyZoomDown = value; }
        }
        public static MyKey KeyMoveCameraLeft
        {
            get { return MyKeyboard.keyMoveCameraLeft; }
            set { MyKeyboard.keyMoveCameraLeft = value; }
        }
        public static MyKey KeyMoveCameraRight
        {
            get { return MyKeyboard.keyMoveCameraRight; }
            set { MyKeyboard.keyMoveCameraRight = value; }
        }
        public static MyKey KeyMoveCameraUp
        {
            get { return MyKeyboard.keyMoveCameraUp; }
            set { MyKeyboard.keyMoveCameraUp = value; }
        }
        public static MyKey KeyMoveCameraDown
        {
            get { return MyKeyboard.keyMoveCameraDown; }
            set { MyKeyboard.keyMoveCameraDown = value; }
        }
        public static MyKey KeyResUp
        {
            get { return MyKeyboard.keyResUp; }
            set { MyKeyboard.keyResUp = value; }
        }
        public static MyKey KeyResDown
        {
            get { return MyKeyboard.keyResDown; }
            set { MyKeyboard.keyResDown = value; }
        }
        public static MyKey KeyFullscreen
        {
            get { return MyKeyboard.keyFullscreen; }
            set { MyKeyboard.keyFullscreen = value; }
        }

        public static void Update()
        {
            keysState = Keyboard.GetState();
            for (int i = 0; i < allKeys.Count; i++)
            {
                allKeys[i].Update(keysState);
            }
        }
    }
}
