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


        public static List<MyKey> AllKeys
        {
            get { return MyKeyboard.allKeys; }
            set { MyKeyboard.allKeys = value; }
        }
        public static MyKey KeyExitGame
        {
            get { return MyKeyboard.keyExitGame; }
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
