using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explicatio.Worlds;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Explicatio.Controls
{
    public static class MyMouse
    {
        private static bool middleButtonStatus = false;
        public static int MouseHoldPositionX { get; private set; }
        public static int MouseHoldPositionY { get; private set; }
        public static int OverallScrollWheelValue { get; private set; }
        /// <summary>
        /// Różnica obrotów kółkiem od ostatniego Update 
        /// Uwaga! Jeden obrót to 120
        /// </summary>
        public static int ScrollWheelDelta { get; private set; }

        private static Vector2 positionRelative;

        public static Vector2 PositionRelative
        {
            get { return MyMouse.positionRelative; }
            set { MyMouse.positionRelative = value; }
        }
        /// <summary>
        /// Update relatywnej pozycji myszy i kółka //!TEMP Tworzy śnieg po wciśnięciu LPM
        /// </summary>
        public static void Update(World world)
        {
            ScrollWheelDelta = OverallScrollWheelValue - Mouse.GetState().ScrollWheelValue;
            OverallScrollWheelValue = Mouse.GetState().ScrollWheelValue;
            positionRelative.X = Rendering.Camera.Transform.Translation.X * -1 * (float)Math.Pow(Rendering.Camera.Zoom, -1) + Mouse.GetState().X * (float)Math.Pow(Rendering.Camera.Zoom, -1);
            positionRelative.Y = Rendering.Camera.Transform.Translation.Y * -1 * (float)Math.Pow(Rendering.Camera.Zoom, -1) + Mouse.GetState().Y * (float)Math.Pow(Rendering.Camera.Zoom, -1);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Interaction(world);
            }
        }

        /// <summary>
        /// Oblicza pozycje myszy względem świata i chunka i //!TEMP Zamienia pole na śnieg
        /// //TODO Dodanie podświetlania pola
        /// </summary>
        public static void Interaction(this World world)
        {
            //relatywna pozycja myszy względem rysowanych chunków
            float cy = (((MyMouse.PositionRelative.Y / 16) - (MyMouse.PositionRelative.X / 32)) / 2) + (world.ChunksInRow * Chunk.CHUNK_SIZE / 2) + 8.5f; //Podczas zmiany sprawdzić dlaczego 8.5 jest potrzebne do dokładności
            float cx = ((MyMouse.PositionRelative.X / 32) + ((MyMouse.PositionRelative.Y / 16) - (MyMouse.PositionRelative.X / 32)) / 2) - (world.ChunksInRow * Chunk.CHUNK_SIZE / 2) - 8.5f;
            //relatywna pozycja myszy względem pola w chunku
            int mx = (int)cx % Chunk.CHUNK_SIZE;
            int my = (int)cy % Chunk.CHUNK_SIZE;
            //relatywna pozycja myszy względem chunka na świecie
            int gx = (int)Math.Floor((double)(cx / Chunk.CHUNK_SIZE));
            int gy = (int)Math.Floor((double)(cy / Chunk.CHUNK_SIZE));
            if (gx >= 0 && gy >= 0 && gx < world.ChunksInRow && gy < world.ChunksInRow)
            {
                Chunk c = world.GetChunk((int)Math.Floor((double)(cx / Chunk.CHUNK_SIZE)), (int)Math.Floor((double)(cy / Chunk.CHUNK_SIZE)));

                if (mx >= 0 && my >= 0 && mx < Chunk.CHUNK_SIZE && my < Chunk.CHUNK_SIZE)
                {
                    //TODO Dodanie odpowieniej metody tworzącej
                    c[(ushort)(mx), (ushort)(my)] = 2;
                    c.MarkToRedraw();
                }
            }

        }

        public static bool ChceckMouseRectangle(int x1, int y1, int x2, int y2)
        {
            if (Mouse.GetState().X >= x1 && Mouse.GetState().X <= x2 && Mouse.GetState().Y >= y1 && Mouse.GetState().Y <= y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ChceckMouseRectangleRelative(int x1, int y1, int x2, int y2)
        {
            if (positionRelative.X >= x1 && positionRelative.X <= x2 && positionRelative.Y >= y1 && positionRelative.Y <= y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ChceckMouseDiamond(int x1, int y1, int x2, int y2)
        {
            if (positionRelative.X >= x1 && positionRelative.X <= x2 && positionRelative.Y >= y1 && positionRelative.Y <= y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Fukncja pomocnicza służy do przesuwania obrazu po przytrzymaniu środkowego przycisku myszy
        /// </summary>
        /// <returns>True - ON, False - OFF</returns>
        public static bool ToogleMiddleButton()
        {
            if (middleButtonStatus == false && Mouse.GetState().MiddleButton == ButtonState.Pressed)
            {
                MouseHoldPositionX = Mouse.GetState().X;
                MouseHoldPositionY = Mouse.GetState().Y;
                middleButtonStatus = true;
            }
            else if (middleButtonStatus == true && Mouse.GetState().MiddleButton == ButtonState.Released)
            {
                middleButtonStatus = false;
            }
            return middleButtonStatus;
        }
    }
}
