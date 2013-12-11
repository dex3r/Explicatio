using System;
using System.Collections.Generic;
using System.Linq;

namespace Explicatio.Main
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameMain())
                game.Run();
        }
    }
}
