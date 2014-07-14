using System;
using System.Collections.Generic;
using System.Text;
using DexGUI.Rendering;

namespace DexGUI
{
    public static class GUIManager
    {
        private static IRenderer renderer;
        internal static List<GUIScreen> Screens { get; private set; }
        private static bool wasInitialized;

         //!? Static properties region
        #region PROPERTIES
        public static IRenderer Renderer
        {
            get { return GUIManager.renderer; } 
            set 
            { 
                GUIManager.renderer = value;
                renderer.Initialize();
            }
        }
        #endregion
        //!? END of static properties region

        /// <summary>
        /// Initialize DexGUI system. Should be called once.
        /// </summary>
        /// <param name="renderer">Class that will be used for rendering the GUI.</param>
        public static void Initialize(IRenderer renderer)
        {
            if(wasInitialized)
            {
                throw new Exception("Initialize method can be called only once");
            }
            wasInitialized = true;
            Screens = new List<GUIScreen>();

            GUIManager.Renderer = renderer;

            renderer.Initialize();
        }

        public static void Update(UpdateState updateState)
        {
           for(int i = 0; i < Screens.Count; i++)
           {
               Screens[i].Update(updateState);
           }
        }

        public static void Draw()
        {
            Renderer.PreDraw();
            for (int i = 0; i < Screens.Count; i++)
            {
                Screens[i].Draw();
            }
            Renderer.PostDraw();
        }

        public static void AddScreen(GUIScreen screen)
        {
            Screens.Add(screen);
            Screens.Sort();
        }

        public static void RemoveScreen(GUIScreen screen)
        {
            Screens.Remove(screen);
        }
    }
}
