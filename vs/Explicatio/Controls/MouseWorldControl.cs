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
        private static short dir;

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
                    Chunk c = world.GetChunk(gx, gy);

                    if (mx >= 0 && my >= 0 && mx < Chunk.CHUNK_SIZE && my < Chunk.CHUNK_SIZE)
                    {
                        createNewSelection(c, gx, gy, mx, my);

                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            //c[(ushort)(mx), (ushort)(my)] = Blocks.Block.Road.Id;
                            //Blocks.Block.Road.SetMetaAuto(world, c, mx, my);
                            saveSelection();

                        }
                        if (Mouse.GetState().RightButton == ButtonState.Pressed)
                        {
                            c[(ushort)(mx), (ushort)(my)] = 1;
                            Blocks.Block.Road.SetMetaAuto(world, c, mx, my, false, true);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Tworzenie zaznaczenia 
        /// </summary>
        private static void createNewSelection(Chunk chunk, int chunkX, int chunkY, int blockX, int blockY)
        {
            actMouseOverBlockX = (ushort)(blockX);
            actMouseOverBlockY = (ushort)(blockY);
            actChunk = new Point(chunkX, chunkY);
        }
        private static void saveSelection()
        {
            lastMouseOverBlockX = actMouseOverBlockX;
            lastMouseOverBlockY = actMouseOverBlockY;
            lastChunk = actChunk;
        }

        public static void DrawSelected(World world)
        {
            if (LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX && LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY)
            {
                for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x++)
                {
                    for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y++)
                    {
                        drawField(x, y, world);
                    }
                }
            }
            else if (LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX >= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX && LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY)
            {
                for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x >= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x--)
                {
                    for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y <= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y++)
                    {
                        drawField(x, y, world);
                    }
                }
            }
            else if (LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX && LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY >= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY)
            {
                for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x <= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x++)
                {
                    for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y >= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y--)
                    {
                        drawField(x, y, world);
                    }
                }
            }
            else if (LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX >= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX && LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY >= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY)
            {
                for (int x = LastChunk.X * Chunk.CHUNK_SIZE + LastMouseOverBlockX; x >= ActChunk.X * Chunk.CHUNK_SIZE + ActMouseOverBlockX; x--)
                {
                    for (int y = LastChunk.Y * Chunk.CHUNK_SIZE + LastMouseOverBlockY; y >= ActChunk.Y * Chunk.CHUNK_SIZE + ActMouseOverBlockY; y--)
                    {
                        drawField(x, y, world);
                    }
                }
            }
        }

        private static void drawField(int x, int y, World world)
        {
            var chunk = (Main.GameMain.CurrentWorld.GetChunk(x / Chunk.CHUNK_SIZE, y / Chunk.CHUNK_SIZE)); //JEŚLI DZIAŁA ŹLE DODAC MATH.FLOOR!!
            var varX = (ushort)(x % 16);
            var varY = (ushort)(y % 16);
            Main.GameMain.SpriteBatch.Draw(Block.Blocks[chunk[varX, varY]].GetTexture(chunk, varX, varY), //Pobiera blok który jest pod myszą
                                           new Vector2(((Chunk.CHUNK_SIZE - varY + varX) * 32) + ((world.ChunksInRow - chunk.Y + chunk.X) * (Chunk.CHUNK_SIZE * 32)),
                                                      ((varX + varY) * 16) + ((chunk.X + chunk.Y) * (Chunk.CHUNK_SIZE * 16))),
                                                      Color.Gainsboro);
        }
    }
}
