﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Explicatio
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Main())
                game.Run();
        }
    }
}
