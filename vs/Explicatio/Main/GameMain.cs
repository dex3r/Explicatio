using System;
using System.Text;
using Explicatio.Controls;
using Explicatio.Rendering;
using Explicatio.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Explicatio.Main
{
    public class GameMain : Game
    {
        #region static
        //!? Private:
        private static GraphicsDeviceManager graphicsDeviceManager;

        // Zmienne do prostego pomiaru FPS
        private static int currentFps;
        private static int lastFps;
        private static long lastSec;
        private static int updateTime;
        //! Wyodrębnienie poza Draw dla wydajności
        private static StringBuilder sb = new StringBuilder();

        //!? Public:
        private static World currentWorld;
        /// <summary>
        /// Świat który jest aktualnie wyświetlany (null jeżeli w menu etc)
        /// </summary>
        public static World CurrentWorld
        {
            get { return currentWorld; }
            set { currentWorld = value; }
        }

        public static SpriteBatch SpriteBatch { get; private set; }
        public static int LastDrawedChunksCount { get; set; }

        /// <summary>
        /// Wróć do pozycji kamery
        /// </summary>
        public static void BeginNormalDrawing()
        {
            BeginDrawingAndApplyTransformation(Matrix.Identity);
        }

        /// <summary>
        /// Zastosuj transofrmację (trans * Camera)
        /// </summary>
        /// <param name="transformation"></param>
        public static void BeginDrawingAndApplyTransformation(Matrix transformation)
        {
            SamplerState.PointWrap.MaxAnisotropy = 0;
            SamplerState.PointWrap.MaxMipLevel = 0;
            SamplerState.PointWrap.MipMapLevelOfDetailBias = 0;
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointWrap, null, null, null, transformation * Camera.Transform);
        }
        #endregion

        public GameMain()
            : base()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            // Ustawianie fullscreena początkowego i rozdziałki jest teraz w obiekcjie options
            // Nie przenosić do Initialize!
            Options.Init(graphicsDeviceManager);
            // Vsync i fixedTimeStep:
            this.IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
            currentWorld = new World();
            //Ustawienie pozycji okna
            Window.SetPosition(new Point(400, 100));
            //Początkowa pozycja kamery na środku rysowanego pola 
            Camera.X = currentWorld.ChunksInRow * Chunk.CHUNK_SIZE * 32;
            Camera.Y = currentWorld.ChunksInRow * Chunk.CHUNK_SIZE * 16;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Textures.Textures.Load(this.Content);
            Text.Load(SpriteBatch, this.Content);
            Text.LoadDefaultFont();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            Options.KeyPressed(graphicsDeviceManager);

            Camera.Interaction(graphicsDeviceManager, GraphicsDevice);
            MyMouse.Update(currentWorld);
            Camera.Update(GraphicsDevice);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (lastSec != (long)gameTime.TotalGameTime.TotalSeconds)
            {
                lastFps = currentFps;
                currentFps = 0;
                lastSec = (long)gameTime.TotalGameTime.TotalSeconds;
            }
            currentFps++;

            //Wyświetlanie po transformacji
            BeginNormalDrawing();
            // Rysowanie świata i obiektów
            GlobalRenderer.Draw(gameTime);
            SpriteBatch.End();

            //Wyświetlanie bez transformacji
            SpriteBatch.Begin();
#if DEBUG
            if (!Keyboard.GetState().IsKeyDown(Keys.F2))
#else
            if (Keyboard.GetState().IsKeyDown(Keys.F2))
#endif
            {
                createDebugInfo();
                Text.DrawTextWithShaddow(Text.Log, new Vector2(0, 0));
                Text.Log = "";
            }
            SpriteBatch.End();
            MyMouse.Interaction(currentWorld);
            base.Draw(gameTime);
        }

        private void createDebugInfo()
        {
            sb.Clear();
            sb.Append("Mouse: ");
            sb.Append(Mouse.GetState().X);
            sb.Append(" ");
            sb.Append(Mouse.GetState().Y);
            sb.Append("\nFps: ");
            sb.Append(lastFps);
            sb.Append("\nRes:");
            sb.Append(GraphicsDevice.Viewport.Width);
            sb.Append("x");
            sb.Append(GraphicsDevice.Viewport.Height);
            sb.Append("\nCamera: ");
            sb.Append(Camera.X);
            sb.Append(" ");
            sb.Append(Camera.Y);
            sb.Append(" Zoom: ");
            sb.Append(Camera.Zoom);
            sb.Append("\nDrawed chunks: ");
            sb.Append(LastDrawedChunksCount);
            sb.Append("\n");
            sb.Append(Text.Log);
            Text.Log = sb.ToString();
        }
    }

}
