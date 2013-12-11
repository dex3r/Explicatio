﻿using Explicatio.Controls;
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
    public class GameMain : Game
    {
        #region static
        public static GameMain Instance { get; private set; }
        public static SpriteBatch SpriteBatch { get; private set; }
        #endregion

        private GameWindow gameWindow;
        private GraphicsDeviceManager graphicsDeviceManager;

        private World currentWorld;
        public World CurrentWorld
        {
            get { return currentWorld; }
            set { currentWorld = value; }
        }

        private int currentFps;
        private int lastFps;
        private long lastSec;

        public GameMain()
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
            //Ustawienie pozycji okna
            Window.SetPosition(new Point(400,100));
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
            GlobalRenderer.Draw(gameTime);
            MouseRelative.MouseObject(GraphicsDevice, this);
            SpriteBatch.End();
            //Wyświetlanie bez transformacji
            SpriteBatch.Begin();
            //if (Keyboard.GetState().IsKeyDown(Keys.F2))
            //{
            Text.Log = "Mouse: " + Mouse.GetState().X + " " + Mouse.GetState().Y + "\n" +       
                       "Fps: " + lastFps + "\n" +
                       "Resolution: " + GraphicsDevice.Viewport.Width + " " + GraphicsDevice.Viewport.Height + "\n" +
                       "Camera: " + Camera.X + " " + Camera.Y + " Zoom: " + Camera.Zoom + "\n" +
                       Camera.Transform.Translation + "\n" +
                       Window.GetForm().Bounds + "\n"
                       //Window.GetForm().WindowState + "\n" +
                       //Window.GetForm().WindowBorder + "\n"
                       //Window.GetForm().WindowInfo 
            ;
            Text.Draw(Text.Log, new Vector2(0, 0), Color.Black, 0.5f);
            //}
            SpriteBatch.End();
            base.Draw(gameTime);
        }

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
    }

}
