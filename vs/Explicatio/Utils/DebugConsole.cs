﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explicatio.Graphics;
using Explicatio.Utils;
using Explicatio.Graphics.Shaders;
using Explicatio.Graphics.Primitives;
using Explicatio.Rendering;
using Explicatio.Controls;
using Explicatio.Worlds;
using Explicatio.Main;

namespace Explicatio.Utils
{
    public static class DebugConsole
    {
        public static StringBuilder DisplayString = new StringBuilder();

        private static int fpsTimer;
        private static int fps, dps;
        public static void Show()
        {

//#if DEBUG
            fpsTimer++; if (fpsTimer >= 4) { fps = (int)Display.Instance.RenderFrequency; dps = (int)Display.Instance.UpdateFrequency; fpsTimer = 0; }
            Console.Title = "Explicatio INDEV  Render Frequency: " + fps + " Update Frequency: " + dps;
            Display.Instance.Title = "Explicatio INDEV  Render Frequency: " + fps + " Update Frequency: " + dps;
//#endif

            Console.Clear();
            DisplayString.Clear();

            DisplayString.Append("Mouse absolutive: ");
            DisplayString.AppendLine(MyMouse.X + " " + MyMouse.Y);
            DisplayString.Append("Mouse client pos: ");
            DisplayString.AppendLine(MyMouse.XClient + " " + MyMouse.YClient);
            DisplayString.Append("Mouse world pos: ");
            DisplayString.AppendLine(MyMouse.XWorld + " " + MyMouse.YWorld);
            DisplayString.Append("Mouse block: ");
            DisplayString.AppendLine(MyMouse.XBlock + " " + MyMouse.YBlock);
            DisplayString.Append("Mouse in boundry: ");
            DisplayString.AppendLine(GameMain.CurrentWorld.CheckMapBoundry() + " ");
            DisplayString.Append("Camera position: ");
            DisplayString.AppendLine(Camera.PosX + " " + Camera.PosY);
            DisplayString.Append("Camera size: ");
            DisplayString.AppendLine(Camera.width + " " + Camera.height);
            DisplayString.Append("Camera zoom: ");
            DisplayString.AppendLine(Camera.Zoom + " ");

            Console.Write(DebugConsole.DisplayString);
        }
    }
}
