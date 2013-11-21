using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

using Explicatio.Rendering;
using Explicatio.Entities;
using Explicatio.Controls;
using Explicatio.Worlds;
using Explicatio.Utils;

namespace Explicatio.Main
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Camera camera;

        private bool fullScreen;

        public static Main Instance { get; private set; }

        private World currentWorld;
        public World CurrentWorld
        {
            get { return currentWorld; }
            set { currentWorld = value; }
        }


        public Main()
            : base()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //this.graphics.PreferredBackBufferHeight = 1080;
            //this.graphics.PreferredBackBufferWidth = 1920;
            //! Ustawianie fullscreena
            fullScreen = true;
            this.graphics.IsFullScreen = fullScreen;
        }

        protected override void Initialize()
        {
            base.Initialize();
            graphics.SynchronizeWithVerticalRetrace = true;
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

            //TODO Nie działają oba :(
            if (Keyboard.GetState().IsKeyDown(Keys.F4))
            {
                //this.graphics.ToggleFullScreen();
                if (fullScreen == true) { fullScreen = false; graphics.IsFullScreen = fullScreen; }
                else { fullScreen = true; graphics.IsFullScreen = fullScreen; }
            }
            const int step = 7;
            const int bordersize = 15; //Wielkość przesuwaka?

            if (MyMouse.ToogleMiddleButton() == false)
            {
                MyMouse.ScrollWheelMoveUpdate();
                camera.Zoom += 0.05f * camera.Zoom * (-MyMouse.ScrollWheelDelta / 60);
                //! Już nie trzeba nic zmieniać żeby działało jak coś się zmieni z rozdzielczością ale generalnie kod jest teraz całkeim nieczytelny. TODO Delete this comment
                if (MyMouse.ChceckMouse(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height / GraphicsDevice.Viewport.Height * bordersize)) camera.Y -= step / camera.Zoom;
                if (MyMouse.ChceckMouse(0, GraphicsDevice.Viewport.Height - GraphicsDevice.Viewport.Height / GraphicsDevice.Viewport.Height * bordersize, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)) camera.Y += step / camera.Zoom;
                if (MyMouse.ChceckMouse(0, 0, GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Width * bordersize, GraphicsDevice.Viewport.Height)) camera.X -= step / camera.Zoom;
                if (MyMouse.ChceckMouse(GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Width * bordersize, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)) camera.X += step / camera.Zoom;
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            camera.UpdateCamera();

            //Wyświetlanie po transformacji
            BeginNormalDrawing();
            GlobalRenderer.Draw(spriteBatch, gameTime);
            spriteBatch.End();
            //Wyświetlanie bez transformacji
            spriteBatch.Begin();
            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                Text.Log = "Mouse: " + Mouse.GetState().X + " " + Mouse.GetState().Y + "\n" +
                           "Fps: " + (1000 / gameTime.ElapsedGameTime.Milliseconds) + "\n" 
                           ;
                Text.Draw(Text.Log, new Vector2(0, 0), Color.Black, 0.5f);
            }
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
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformation * camera.Transform);
        }
    }

}
