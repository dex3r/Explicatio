using Explicatio.Blocks;
using Explicatio.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Controls
{
    public static class MouseWorldControl
    {
        #region Draw Stuff
        /// <summary>
        /// Poprzednio podświetlane pole
        /// </summary>
        private static Point lastChunk;
        public static Point LastChunk
        {
            get { return lastChunk; }
        }
        private static ushort lastMouseOverBlockX;
        public static ushort LastMouseOverBlockX
        {
            get { return lastMouseOverBlockX; }
        }
        private static ushort lastMouseOverBlockY;
        public static ushort LastMouseOverBlockY
        {
            get { return lastMouseOverBlockY; }
        }

        private static Point actChunk;
        public static Point ActChunk
        {
            get { return actChunk; }
        }
        private static ushort actMouseOverBlockX;
        public static ushort ActMouseOverBlockX
        {
            get { return actMouseOverBlockX; }
        }
        private static ushort actMouseOverBlockY;
        public static ushort ActMouseOverBlockY
        {
            get { return actMouseOverBlockY; }
        }
        private static byte drawSelection = 0;
        public static byte DrawSelection
        {
            get { return drawSelection; }
        }
        private static byte selectionDir = 0;
        #endregion

        /// <summary>
        /// Oblicza pozycje myszy względem świata i chunka i //!TEMP Zamienia pole na śnieg
        /// //TODO Dodanie podświetlania pola
        /// </summary>
        public static void Interaction(this World world)
        {
            //if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //relatywna pozycja myszy względem rysowanych chunków
                float cy = (((MyMouse.PositionRelative.Y / 16) - (MyMouse.PositionRelative.X / 32)) / 2) + (world.ChunksInRow * Chunk.CHUNK_SIZE / 2) + 8.49f; //Bloki podczas rysowania przesunięte są o 0.25 w dół i 0.75 w lewo (sprite)
                float cx = ((MyMouse.PositionRelative.X / 32) + ((MyMouse.PositionRelative.Y / 16) - (MyMouse.PositionRelative.X / 32)) / 2) - (world.ChunksInRow * Chunk.CHUNK_SIZE / 2) - 8.51f;
                //relatywna pozycja myszy względem pola w chunku
                int mx = (int)cx % Chunk.CHUNK_SIZE;
                int my = (int)cy % Chunk.CHUNK_SIZE;
                //relatywna pozycja myszy względem chunka na świecie
                int gx = (int)Math.Floor((double)(cx / Chunk.CHUNK_SIZE));
                int gy = (int)Math.Floor((double)(cy / Chunk.CHUNK_SIZE));
                if (gx >= 0 && gy >= 0 && gx < world.ChunksInRow && gy < world.ChunksInRow)
                {

                    if (mx >= 0 && my >= 0 && mx < Chunk.CHUNK_SIZE && my < Chunk.CHUNK_SIZE)
                    {
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            createNewSelection(gx, gy, mx, my);
                            if (drawSelection == 0)
                            {
                                drawSelection = 1;
                                saveSelection();
                            }
                        }
                        else
                        {
                            if (drawSelection == 1)
                            {
                                drawSelection = 0;
                                createRoad(world);
                            }
                        }
                        if (Mouse.GetState().RightButton == ButtonState.Pressed)
                        {
                            createNewSelection(gx, gy, mx, my);
                            createBlock(actMouseOverBlockX, actMouseOverBlockY, Blocks.Block.Grass.Id, world);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Tworzenie zaznaczenia 
        /// </summary>
        private static void createNewSelection(int chunkX, int chunkY, int blockX, int blockY)
        {
            actMouseOverBlockX = (ushort)(blockX);
            actMouseOverBlockY = (ushort)(blockY);
            actChunk = new Point(chunkX, chunkY);
            calculateSelectionDir();
        }
        /// <summary>
        /// Zapisanie zaznaczenia
        /// </summary>
        private static void saveSelection()
        {
            lastMouseOverBlockX = actMouseOverBlockX;
            lastMouseOverBlockY = actMouseOverBlockY;
            lastChunk = actChunk;
        }

        public static void DrawSelected(World world)
        {
            Rendering.Text.Log += selectionDir;
            if (selectionDir == 0)
            {
                for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x++)
                {
                    drawField(x, LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY, Color.Gainsboro, world);
                }
                if (LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY)
                {
                    for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y++)
                    {
                        drawField(ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX, y, Color.Gainsboro, world);
                    }
                }
                else
                {
                    for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y >= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y--)
                    {
                        drawField(ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX, y, Color.Gainsboro, world);
                    }
                }
            }
            else if (selectionDir == 2)
            {
                for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x >= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x--)
                {
                    drawField(x, LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY, Color.Gainsboro, world);
                }
                if (LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY)
                {
                    for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y++)
                    {
                        drawField(ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX, y, Color.Gainsboro, world);
                    }
                }
                else
                {
                    for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y >= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y--)
                    {
                        drawField(ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX, y, Color.Gainsboro, world);
                    }
                }
            }
            else if (selectionDir == 1)
            {
                for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y++)
                {
                    drawField(LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX, y, Color.Gainsboro, world);
                }
                if (LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX)
                {
                    for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x++)
                    {
                        drawField(x, ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY, Color.Gainsboro, world);
                    }
                }
                else
                {
                    for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x >= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x--)
                    {
                        drawField(x, ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY, Color.Gainsboro, world);
                    }
                }
            }
            else if (selectionDir == 3)
            {
                for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y >= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y--)
                {
                    drawField(LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX, y, Color.Gainsboro, world);
                }
                if (LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX)
                {
                    for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x++)
                    {
                        drawField(x, ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY, Color.Gainsboro, world);
                    }
                }
                else
                {
                    for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x >= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x--)
                    {
                        drawField(x, ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY, Color.Gainsboro, world);
                    }
                }
            }
        }

        private static void createRoad(World world)
        {
            if (selectionDir == 0)
            {
                for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x++)
                {
                    createBlock(x, LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY, Block.Road.Id, world);
                }
                if (LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY)
                {
                    for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y++)
                    {
                        createBlock(ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX, y, Block.Road.Id, world);
                    }
                }
                else
                {
                    for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y >= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y--)
                    {
                        createBlock(ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX, y, Block.Road.Id, world);
                    }
                }
            }
            else if (selectionDir == 2)
            {
                for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x >= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x--)
                {
                    createBlock(x, LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY, Block.Road.Id, world);
                }
                if (LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY)
                {
                    for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y++)
                    {
                        createBlock(ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX, y, Block.Road.Id, world);
                    }
                }
                else
                {
                    for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y >= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y--)
                    {
                        createBlock(ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX, y, Block.Road.Id, world);
                    }
                }
            }
            else if (selectionDir == 1)
            {
                for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y++)
                {
                    createBlock(LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX, y, Block.Road.Id, world);
                }
                if (LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX)
                {
                    for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x++)
                    {
                        createBlock(x, ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY, Block.Road.Id, world);
                    }
                }
                else
                {
                    for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x >= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x--)
                    {
                        createBlock(x, ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY, Block.Road.Id, world);
                    }
                }
            }
            else if (selectionDir == 3)
            {
                for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y >= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y--)
                {
                    createBlock(LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX, y, Block.Road.Id, world);
                }
                if (LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX)
                {
                    for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x++)
                    {
                        createBlock(x, ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY, Block.Road.Id, world);
                    }
                }
                else
                {
                    for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x >= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x--)
                    {
                        createBlock(x, ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY, Block.Road.Id, world);
                    }
                }
            }
        }

        private static void drawField(int x, int y, Color color, World world)
        {
            if (drawSelection >= 1)
            {
                var chunk = (Main.GameMain.CurrentWorld.GetChunk(x / Chunk.CHUNK_SIZE, y / Chunk.CHUNK_SIZE));
                var varX = (ushort)(x % 16);
                var varY = (ushort)(y % 16);
                Main.GameMain.SpriteBatch.Draw(Block.Blocks[chunk[varX, varY]].GetTexture(chunk, varX, varY), //Pobiera blok który jest pod myszą
                                               new Vector2(((Chunk.CHUNK_SIZE - varY + varX) * 32) + ((world.ChunksInRow - chunk.Y + chunk.X) * (Chunk.CHUNK_SIZE * 32)),
                                                          ((varX + varY) * 16) + ((chunk.X + chunk.Y) * (Chunk.CHUNK_SIZE * 16))),
                                                          color);
            }
        }
        private static void createBlock(int x, int y, byte blockType, World world)
        {
            var chunk = (Main.GameMain.CurrentWorld.GetChunk(x / Chunk.CHUNK_SIZE, y / Chunk.CHUNK_SIZE));
            chunk[(ushort)(x%16), (ushort)(y%16)] = blockType;
            Blocks.Block.Road.SetMetaAuto(world, chunk, x % 16, y % 16);
        }

        private static void calculateSelectionDir()
        {
            if (LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX && LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY == ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY)
            {
                selectionDir = 0;
            }
            else if (LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX > ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX && LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY == ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY)
            {
                selectionDir = 2;
            }
            else if (LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY && LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX == ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX)
            {
                selectionDir = 1;
            }
            else if (LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY > ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY && LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX == ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX)
            {
                selectionDir = 3;
            }

            if (selectionDir == 0 && LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX > ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX)
            {
                selectionDir = 2;
            }
            if (selectionDir == 2 && LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX)
            {
                selectionDir = 0;
            }
            if (selectionDir == 1 && LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY > ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY)
            {
                selectionDir = 3;
            }
            if (selectionDir == 3 && LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY)
            {
                selectionDir = 1;
            }
        }
    }
}
