using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explicatio.Controls;

namespace Explicatio.Main
{
    public static class Console
    {
        public static bool isVisible = false;

        static Console()
        {
            MyKeyboard.KeyToggleConsole.ButtonToggleOnEvent += new ButtonChangeState(ShowConsoleKeyDown);
        }
         
        public static void ShowConsoleKeyDown(MyKey key)
        {
            if(isVisible)
            {
                isVisible = false;
            }
            else
            {
                isVisible = true;
            }
        }
    }
}
