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

namespace Explicatio.Main
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Camera camera; float zoom = 1;
        float i = 0,x=0,y=0;

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
            camera = new Camera(new Viewport(0,0,GraphicsDevice.Viewport.Width,GraphicsDevice.Viewport.Height));
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
            //TODO Przeniść w fajne miejsce
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                zoom -= 0.01f;
            else if (Mouse.GetState().RightButton == ButtonState.Pressed)
                zoom += 0.01f;
            if(Keyboard.GetState().IsKeyDown(Keys.Left)) x++;
            if(Keyboard.GetState().IsKeyDown(Keys.Right)) x--;
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) y--;
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) y++;
            camera.Update(gameTime, i+=0.05f, new Vector2(GraphicsDevice.Viewport.Width/2+x, GraphicsDevice.Viewport.Height/2+y), zoom);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Viewport = camera.View;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, camera.Transform);
            GlobalRenderer.Draw(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
