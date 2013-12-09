using Explicatio.Controls;
using Explicatio.Entities;
using Explicatio.Rendering;
using Explicatio.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Explicatio.Main
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch spriteBatch;
        private Camera camera;

        public static Main Instance { get; private set; }

        private World currentWorld;
        public World CurrentWorld
        {
            get { return currentWorld; }
            set { currentWorld = value; }
        }

        private int currentFps;
        private int lastFps;
        private long lastSec;

        public Main()
            : base()
        {
            Instance = this;
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Ustawianie fullscreena początkowego i rozdziałki jest teraz w obiekcjie options
            Options.Init(graphicsDeviceManager);
            this.IsFixedTimeStep = false;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
            
        }

        protected override void Initialize()
        {
            base.Initialize();
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            currentWorld = new World();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Textures.Load(this.Content);
            camera = new Camera(new Viewport(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            Text.Load(spriteBatch,this.Content);
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

            const int step = 7;
            const int bordersize = 15; //Wielkość przesuwaka?

            if (MyMouse.ToogleMiddleButton() == false)
            {
                MyMouse.ScrollWheelMoveUpdate();
                camera.Zoom += 0.25f * camera.Zoom * (-MyMouse.ScrollWheelDelta / 120);
                //camera.Zoom = Math.Min(4.5f, Math.Max(0.01f, (float)Math.Round(camera.Zoom, 1)));
                //camera.Zoom += 0.2f * (-MyMouse.ScrollWheelDelta / 120);
                //! Już nie trzeba nic zmieniać żeby działało jak coś się zmieni z rozdzielczością ale generalnie kod jest teraz całkeim nieczytelny. TODO Delete this comment
                //if (MyMouse.ChceckMouseRectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height / GraphicsDevice.Viewport.Height * bordersize)) camera.Y -= step / camera.Zoom;
                //if (MyMouse.ChceckMouseRectangle(0, GraphicsDevice.Viewport.Height - GraphicsDevice.Viewport.Height / GraphicsDevice.Viewport.Height * bordersize, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)) camera.Y += step / camera.Zoom;
                //if (MyMouse.ChceckMouseRectangle(0, 0, GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Width * bordersize, GraphicsDevice.Viewport.Height)) camera.X -= step / camera.Zoom;
                //if (MyMouse.ChceckMouseRectangle(GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Width * bordersize, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)) camera.X += step / camera.Zoom;
                if (Keyboard.GetState().IsKeyDown(Keys.Left)) camera.X -= step / camera.Zoom;
                if (Keyboard.GetState().IsKeyDown(Keys.Right)) camera.X += step / camera.Zoom;
                if (Keyboard.GetState().IsKeyDown(Keys.Up)) camera.Y -= step / camera.Zoom;
                if (Keyboard.GetState().IsKeyDown(Keys.Down)) camera.Y += step / camera.Zoom;
            }
            else
            {
                camera.X -= (MyMouse.MouseHoldPositionX - Mouse.GetState().X) / 40 / camera.Zoom;
                camera.Y -= (MyMouse.MouseHoldPositionY - Mouse.GetState().Y) / 40 / camera.Zoom;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if(lastSec != (long)gameTime.TotalGameTime.TotalSeconds)
            {
                lastFps = currentFps;
                currentFps = 0;
                lastSec = (long)gameTime.TotalGameTime.TotalSeconds;
            }
            currentFps++;
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Red);
            spriteBatch.End();
       
            camera.UpdateCamera();

            //Wyświetlanie po transformacji
            BeginNormalDrawing();
            GlobalRenderer.Draw(spriteBatch, gameTime,camera);
            spriteBatch.End();
            //Wyświetlanie bez transformacji
            spriteBatch.Begin();
            //if (Keyboard.GetState().IsKeyDown(Keys.F2))
            //{
                Text.Log = "Mouse: " + Mouse.GetState().X + " " + Mouse.GetState().Y + "\n" +
                           "Fps: " + (1000 / gameTime.ElapsedGameTime.Milliseconds) + "\n" +
                           "Fps2: " + lastFps + "\n";
                           ;
                Text.Draw(Text.Log, new Vector2(0, 0), Color.Black, 0.5f);
            //}
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Wróć do pozycji kamery
        /// </summary>
        public void BeginNormalDrawing()
        {
            BeginDrawingAndApplyTransformation(Matrix.Identity);
        }

        /// <summary>
        /// Zastosuj transofrmację (trans * camera)
        /// </summary>
        /// <param name="transformation"></param>
        public void BeginDrawingAndApplyTransformation(Matrix transformation)
        {
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformation * camera.Transform);
        }
    }

}
