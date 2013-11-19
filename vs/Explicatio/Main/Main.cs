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

namespace Explicatio.Main
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Camera camera;

        public Main()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            graphics.SynchronizeWithVerticalRetrace = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Textures.Load(this.Content);
            camera = new Camera(new Viewport(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
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

            MyMouse.Update();

            camera.Zoom += 0.05f * camera.Zoom * (-MyMouse.ScrollWheelDelta / 60);
            const int step = 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) camera.X -= step / camera.Zoom;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) camera.X += step / camera.Zoom;
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) camera.Y -= step / camera.Zoom;
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) camera.Y += step / camera.Zoom;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            camera.UpdateCamera();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);
            GlobalRenderer.Draw(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
