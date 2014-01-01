using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Explicatio.Worlds;
using Explicatio.Blocks;
using Explicatio.Main;
using Explicatio.Rendering;
using Explicatio.Utils;
using Explicatio.Controls;

namespace Explicatio.Rendering
{
    public static class GlobalRenderer
    {
        public static void Draw(GameTime time)
        {
            GameMain.SpriteBatch.End();
            World world = Main.GameMain.CurrentWorld;
            Chunk c;
            for (int x = 0; x < world.ChunksInRow; x++)
            {
                for (int y = 0; y < world.ChunksInRow; y++)
                {
                    c = world.GetChunk(x, y);
                    if (c.NeedsRedrawing)
                    {
                        ChunkRenderer.RenderChunk(c);
                    }
                }
            }
            GameMain.BeginNormalDrawing();
            GameMain.SpriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawWorld(Main.GameMain.CurrentWorld);

            MouseWorldControl.DrawSelected(world);
        }

        //! Zmienne globalne dla zwiększenia wydajności
        private static Chunk c;
        private static Chunk lastRenderedChunk;
        private static Vector2 v;
        // const dla wydajności
        private const int o1 = Chunk.CHUNK_SIZE * 32;
        private const int o2 = Chunk.CHUNK_SIZE * 16;
        // Pozycja chunka do wyrenderowania
        private static int mx, my;
        // Pozycja pierwszego widocznego pixela
        private static float startscreenX;
        private static float startscreenY;
        //TODO: Wykonywać pętlę tylko dla widocznych chunków (obliczać) Potem wykonać jeszcze jedną pętlę dla wartości z ostatniego przebiegu (usunięcie chunków)
        private static float startscreenXUnzoomed;
        private static float startscreenYUnzoomed;
        // Pozycja ostatniego widocznego pixela
        private static float endscreenX;
        private static float endscreenY;
        private static float endscreenXUnzoomed;
        private static float endscreenYUnzoomed;

        public static void DrawWorld(World world)
        {
            GameMain.LastDrawedChunksCount = 0;
            // Pozycja pierwszego widocznego pixela
            startscreenX = -Camera.Transform.Translation.X * (1 / Camera.Zoom);
            startscreenY = -Camera.Transform.Translation.Y * (1 / Camera.Zoom);
            //TODO: Wykonywać pętlę tylko dla widocznych chunków (obliczać) Potem wykonać jeszcze jedną pętlę dla wartości z ostatniego przebiegu (usunięcie chunków)
            //int startX = (int)(((startscreenY / o2) - (startscreenX / o1) + world.ChunksInRow) / 2);
            startscreenXUnzoomed = -Camera.CreateVirtualTransofrmation(0.2f).Translation.X * (1 / 0.2f);
            startscreenYUnzoomed = -Camera.CreateVirtualTransofrmation(0.2f).Translation.Y * (1 / 0.2f);
            // Pozycja ostatniego widocznego pixela
            endscreenX = startscreenX + Main.GameMain.SpriteBatch.GraphicsDevice.Viewport.Width * (1 / Camera.Zoom);
            endscreenY = startscreenY + Main.GameMain.SpriteBatch.GraphicsDevice.Viewport.Height * (1 / Camera.Zoom);
            endscreenXUnzoomed = startscreenXUnzoomed + Main.GameMain.SpriteBatch.GraphicsDevice.Viewport.Width * (1 / 0.2f);
            endscreenYUnzoomed = startscreenYUnzoomed + Main.GameMain.SpriteBatch.GraphicsDevice.Viewport.Height * (1 / 0.2f);
            for (int x = 0; x < world.ChunksInRow; x++)
            {
                for (int y = 0; y < world.ChunksInRow; y++)
                {
                    mx = (world.ChunksInRow - y + x) * o1;
                    my = (x + y) * o2;
                    // Rysowanie tylko widocznych chunków
                    if (mx + ChunkRenderer.CHUNK_SURFACE_WIDTH < startscreenX || my + (2 * ChunkRenderer.CHUNK_SURFACE_HEIGHT) < startscreenY || mx > endscreenX || my > endscreenY)
                    {
                        // Usuwanie z pamięci tylko niewidocznych chunków przy max oddapelni (minimalny zoom)
                        if (mx + ChunkRenderer.CHUNK_SURFACE_WIDTH < startscreenXUnzoomed || my + ChunkRenderer.CHUNK_SURFACE_HEIGHT < startscreenYUnzoomed || mx > endscreenXUnzoomed || my > endscreenYUnzoomed)
                        {
                            c = world.GetChunk(x, y);
                            if (c.RenderTarget != null)
                            {
                                c.RenderTarget.Dispose();
                                c.RenderTarget = null;
                            }
                        }
                        continue;
                    }
                    c = world.GetChunk(x, y);
                    if (c.RenderTarget == null)
                    {
                        c.NeedsRedrawing = true;
                        if (lastRenderedChunk != null && lastRenderedChunk.RenderTarget != null)
                        {
                            c = lastRenderedChunk;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        lastRenderedChunk = c;
                    }
                    //Pozycja chunka do narysowania
                    v = new Vector2(mx, my);
                    GameMain.SpriteBatch.Draw(c.RenderTarget, v, Color.White);
                    GameMain.LastDrawedChunksCount++;
                }
            }
        }
    }
}
