using System;
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
            SpriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformation * Camera.Transform);
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
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Textures.Load(this.Content);
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
            MyMouse.Update();
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

            SpriteBatch.Begin();
            GraphicsDevice.Clear(Color.Red);
            SpriteBatch.End();
            //Wyświetlanie po transformacji
            BeginNormalDrawing();
            // Rysowanie świata i obiektów
            GlobalRenderer.Draw(gameTime);
            SpriteBatch.End();
            //? TEMP
            BeginNormalDrawing();
            const int o1 = Chunk.CHUNK_SIZE * 32;
            const int o2 = Chunk.CHUNK_SIZE * 16;
            //mx = (world.ChunksInRow - y + x) * o1;
            //my = (x + y) * o2;
            int y = (int)(((MyMouse.PositionRelative.Y / o2) - (MyMouse.PositionRelative.X / o1) + currentWorld.ChunksInRow) / 2);
            int x = (int)((MyMouse.PositionRelative.X / o1) + ((MyMouse.PositionRelative.Y / o2) - (MyMouse.PositionRelative.X / o1) + currentWorld.ChunksInRow) / 2) - currentWorld.ChunksInRow;
            //int x = (int)((MyMouse.PositionRelative.X  - currentWorld.ChunksInRow +(MyMouse.PositionRelative.Y) / (Chunk.CHUNK_SIZE * 32))) - 1;
            //int y = (int)(MyMouse.PositionRelative.Y / (Chunk.CHUNK_SIZE * 16) - (MyMouse.PositionRelative.X / (Chunk.CHUNK_SIZE * 32))) + currentWorld.ChunksInRow ;
            Text.Draw(x + " " + y, MyMouse.PositionRelative);
            if (x >= 0 && y >= 0 && x < currentWorld.ChunksInRow && y < currentWorld.ChunksInRow)
            {
                Chunk c = currentWorld.GetChunk(x, y);
                for (int i = 0; i < Chunk.CHUNK_SIZE; i++)
                {
                    for (int j = 0; j < Chunk.CHUNK_SIZE; j++)
                    {
                        c[(ushort)i, (ushort)j] = 2;
                    }
                }
                c.MarkToRedraw();
            }
            SpriteBatch.End();

            //Wyświetlanie bez transformacji
            SpriteBatch.Begin();
            if (!Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                Text.Log = "Mouse: " + Mouse.GetState().X + " " + Mouse.GetState().Y + "\n" +
                           "Fps: " + lastFps + "\n" +
                           "Resolution: " + GraphicsDevice.Viewport.Width + " " + GraphicsDevice.Viewport.Height + "\n" +
                           "Camera: " + Camera.X + " " + Camera.Y + " Zoom: " + Camera.Zoom + "\n" +
                           Camera.Transform.Translation + "\n" +
                           Window.GetForm().Bounds + "\n" +
                           "Drawed chunks: " + LastDrawedChunksCount + "\n" +
                           Text.Log
                ;
                Text.Draw(Text.Log, new Vector2(0, 0), Color.Black, 0.5f);
                Text.Log = "";
            }
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }

}
