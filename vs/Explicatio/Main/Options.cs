using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Explicatio.Main
{
    public static class Options
    {

        private static byte resolutionChange = 0;
        /// <summary>
        /// Zmienna pomocnicza do poprawnej zmiany rozdzielczości na pełnym ekranie
        /// </summary>
        public static byte ResolutionChange
        {
            get { return Options.resolutionChange; }
            set { Options.resolutionChange = value; }
        }

        /// <summary>
        /// Status która rozdzielczość jest wybrana ( 0 = największa )
        /// </summary>
        private static int resolutionStatus;

        public static int ResolutionStatus
        {
            get { return Options.resolutionStatus; }
            set { Options.resolutionStatus = value; }
        }
        /// <summary>
        /// Tablica dostępnych rozdzielczości
        /// </summary>
        public static readonly int[,] resolution = new int[15, 2]
        {
            {1920,1080},
            {1680,1050},
            {1600,900},
            {1440,900},
            {1400,1050},
            {1366,768},
            {1360,768},
            {1280,1024},
            {1280,960},
            {1280,800},
            {1280,768},
            {1280,720},
            {1280,600},
            {1152,864},
            {1024,768}
        };
        /// <summary>
        /// Initializowanie opcji gry (narazie jest tylko rozdzielczość)
        /// </summary>
        public static void Init(GraphicsDeviceManager graphicsDeviceManager)
        {
#if DEBUG
            resolutionStatus = 14;
            updateResolution(graphicsDeviceManager, resolution[resolutionStatus, 0], resolution[resolutionStatus, 1]);
#else
            //Rozdzielczość domyślna 1920x1080
            resolutionStatus = 0;
            updateResolution(graphicsDeviceManager, resolution[resolutionStatus, 0], resolution[resolutionStatus, 1]);
            //! Ustawianie FullScreena na początku
            toogleFullScreeen(graphicsDeviceManager);
#endif
        }

        /// <summary>
        /// Funkcja wywołująca akcje na przyskach
        //TODO DO ZMAINY NA GUI!
        /// </summary>
        public static void KeyPressed(GraphicsDeviceManager graphicsDeviceManager)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F4))
            {
                toogleFullScreeen(graphicsDeviceManager);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                if (resolutionStatus < 14)
                {
                    resolutionStatus++;
                    updateResolution(graphicsDeviceManager, resolution[resolutionStatus, 0], resolution[resolutionStatus, 1]);
                    ResolutionChange = 1;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F6))
            {
                if (resolutionStatus > 0)
                {

                    resolutionStatus--;
                    updateResolution(graphicsDeviceManager, resolution[resolutionStatus, 0], resolution[resolutionStatus, 1]);
                    ResolutionChange = 1;
                }
            }
            //Zmiana rozdzielczości w przypadku uruchomionego fullscreena
            updateResolutionOnFullScreenFix(graphicsDeviceManager);
        }

        /// <summary>
        /// Toogle full screen z aplikowaniem zmian
        /// </summary>
        private static void toogleFullScreeen(GraphicsDeviceManager graphicsDeviceManager)
        {
            graphicsDeviceManager.ToggleFullScreen();
            graphicsDeviceManager.ApplyChanges();
        }
        /// <summary>
        /// Ustawianie rozdzielczości
        /// UWAGA! Do zmiany rozdzielczości na fullscreenie nie działa
        /// </summary>
        /// <param name="width">Szerokość ekranu</param>
        /// <param name="height">Wysokość ekranu</param>
        private static void updateResolution(GraphicsDeviceManager graphicsDeviceManager, int width, int height)
        {
            graphicsDeviceManager.PreferredBackBufferHeight = height;
            graphicsDeviceManager.PreferredBackBufferWidth = width;
            graphicsDeviceManager.ApplyChanges();
        }
        /// <summary>
        /// Naprawa błędów wywołanych zmianą rozdzielczości podczas fullScreena
        /// </summary>
        private static void updateResolutionOnFullScreenFix(GraphicsDeviceManager graphicsDeviceManager)
        {
            if (ResolutionChange == 1 && graphicsDeviceManager.IsFullScreen == true)
            {
                toogleFullScreeen(graphicsDeviceManager);
                ResolutionChange = 2;
            }
            if (ResolutionChange == 2)
            {
                toogleFullScreeen(graphicsDeviceManager);
                ResolutionChange = 0;
            }
        }

    }
}
