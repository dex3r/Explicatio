using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Explicatio.Controls
{
    public static class MyKeyboard
    {
        private static List<MyKey> allKeys = new List<MyKey>();
        public static List<MyKey> AllKeys
        {
            get { return MyKeyboard.allKeys; }
            set { MyKeyboard.allKeys = value; }
        }

        private static MyKey keyToggleConsole = new MyKey("Toggle console", Keys.OemTilde);
        public static MyKey KeyToggleConsole
        {
            get { return MyKeyboard.keyToggleConsole; }
        }

        public static void Update()
        {
            KeyboardState kstate = Keyboard.GetState();
            for(int i = 0; i < allKeys.Count; i++)
            {
                allKeys[i].Update(kstate);
            }
        }
    }
}
