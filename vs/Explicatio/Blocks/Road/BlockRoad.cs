using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Explicatio.Worlds;
using Explicatio.Rendering;
using Explicatio.Textures;
using Explicatio.Main;

namespace Explicatio.Blocks
{
    public class BlockRoad : Block
    {

        public BlockRoad(byte id, String name)
            : base(id, name)
        {

        }

        public override Texture2D GetTexture(Chunk c, ushort chunkX, ushort chunkY)
        {
            return TexturesRoads.GetTexture((0xF & c.GetMeta(chunkX, chunkY)), ((0xF0 & c.GetMeta(chunkX, chunkY)) >> 4));
        }

        public void SetMeta(int roadState, int roadType, int x, int y)
        {
            GameMain.CurrentWorld.SetMeta((UInt16)(roadType | (roadState << 4)), x, y);
        }

        public void SetMeta(int roadState, int roadType, Chunk chunk, ushort chunkX, ushort chunkY)
        {
            chunk.SetMeta((UInt16)(roadType | (roadState << 4)), chunkX, chunkY);
        }
        //TODO Dodać pobieranie i ustawianie typu 
        /// <summary>
        /// Ustawianie drogi
        /// </summary>
        /// <param name="chunk">Chunk w którym ma się stworzyć droga</param>
        /// <param name="x"> X pola w chunku</param>
        /// <param name="y"> Y pola w chunku</param>
        /// <param name="step"> Czy zrobić update pobliskich dróg. True = nie, False = tak</param>
        /// <param name="updateOnly"> Czy zrobić tylko update bez zamiany bloku</param>
        public void SetMetaAuto(World world, Chunk chunk, int x, int y, bool step = false, bool updateOnly = false)
        {
            int roadState = 0;
            bool[] neighbor = new bool[4];
            #region Middle
            if (x != 0 && x != Chunk.CHUNK_SIZE - 1 && y != 0 && y != Chunk.CHUNK_SIZE - 1)
            {
                setTopAuto(chunk, world, neighbor, x, y, step);
                setLeftAuto(chunk, world, neighbor, x, y, step);
                setBottomAuto(chunk, world, neighbor, x, y, step);
                setRightAuto(chunk, world, neighbor, x, y, step);

                if (updateOnly == false) { setType(neighbor, roadState, (ushort)x, (ushort)y, chunk); }
            }
            #endregion
            #region Left
            if (x != 0 && x != Chunk.CHUNK_SIZE - 1 && y == 0)
            {
                setTopAuto(chunk, world, neighbor, x, y, step);
                if (chunk.Y != 0)
                {
                    setLeftAutoChunk(chunk, world, neighbor, x, y, step);
                }
                setBottomAuto(chunk, world, neighbor, x, y, step);
                setRightAuto(chunk, world, neighbor, x, y, step);

                if (updateOnly == false) { setType(neighbor, roadState, (ushort)x, (ushort)y, chunk); }
            }
            #endregion
            #region Right
            if (x != 0 && x != Chunk.CHUNK_SIZE - 1 && y == Chunk.CHUNK_SIZE - 1)
            {
                setTopAuto(chunk, world, neighbor, x, y, step);
                setLeftAuto(chunk, world, neighbor, x, y, step);
                setBottomAuto(chunk, world, neighbor, x, y, step);
                if (chunk.Y != world.ChunksInRow - 1)
                {
                    setRightAutoChunk(chunk, world, neighbor, x, y, step);
                }

                if (updateOnly == false) { setType(neighbor, roadState, (ushort)x, (ushort)y, chunk); }
            }
            #endregion
            #region Top
            if (x == 0 && y != 0 && y != Chunk.CHUNK_SIZE - 1)
            {
                if (chunk.X != 0)
                {
                    setTopAutoChunk(chunk, world, neighbor, x, y, step);
                }
                setLeftAuto(chunk, world, neighbor, x, y, step);
                setBottomAuto(chunk, world, neighbor, x, y, step);
                setRightAuto(chunk, world, neighbor, x, y, step);

                if (updateOnly == false) { setType(neighbor, roadState, (ushort)x, (ushort)y, chunk); }
            }
            #endregion
            #region Bottom
            if (x == Chunk.CHUNK_SIZE - 1 && y != 0 && y != Chunk.CHUNK_SIZE - 1)
            {
                setTopAuto(chunk, world, neighbor, x, y, step);
                setLeftAuto(chunk, world, neighbor, x, y, step);
                if (chunk.X != world.ChunksInRow - 1)
                {
                    setBottomAutoChunk(chunk, world, neighbor, x, y, step);
                }
                setRightAuto(chunk, world, neighbor, x, y, step);

                if (updateOnly == false) { setType(neighbor, roadState, (ushort)x, (ushort)y, chunk); }
            }
            #endregion

            #region Top Left
            if (x == 0 && y == 0)
            {
                if (chunk.X != 0)
                {
                    setTopAutoChunk(chunk, world, neighbor, x, y, step);
                } 
                if(chunk.Y != 0)
                {
                    setLeftAutoChunk(chunk, world, neighbor, x, y, step);
                }
                setBottomAuto(chunk, world, neighbor, x, y, step);
                setRightAuto(chunk, world, neighbor, x, y, step);

                if (updateOnly == false) { setType(neighbor, roadState, (ushort)x, (ushort)y, chunk); }
            }
            #endregion
            #region Top Right
            if (x == 0 && y == Chunk.CHUNK_SIZE - 1)
            {
                if (chunk.X != 0)
                {
                    setTopAutoChunk(chunk, world, neighbor, x, y, step);
                }
                setLeftAuto(chunk, world, neighbor, x, y, step);
                setBottomAuto(chunk, world, neighbor, x, y, step);
                if (chunk.Y != world.ChunksInRow - 1)
                {
                    setRightAutoChunk(chunk, world, neighbor, x, y, step);
                }

                if (updateOnly == false) { setType(neighbor, roadState, (ushort)x, (ushort)y, chunk); }
            }
            #endregion
            #region Bottom Right
            if (x == Chunk.CHUNK_SIZE - 1 && y == Chunk.CHUNK_SIZE - 1)
            {
                setTopAuto(chunk, world, neighbor, x, y, step);
                setLeftAuto(chunk, world, neighbor, x, y, step);
                if (chunk.X != world.ChunksInRow - 1 )
                {
                    setBottomAutoChunk(chunk, world, neighbor, x, y, step);
                }
                if (chunk.Y != world.ChunksInRow - 1)
                {
                    setRightAutoChunk(chunk, world, neighbor, x, y, step);
                }

                if (updateOnly == false) { setType(neighbor, roadState, (ushort)x, (ushort)y, chunk); }
            }
            #endregion
            #region Bottom Left
            if (x == Chunk.CHUNK_SIZE - 1 && y == 0)
            {
                setTopAuto(chunk, world, neighbor, x, y, step);
                if (chunk.Y != 0)
                {
                    setLeftAutoChunk(chunk, world, neighbor, x, y, step);
                }
                if (chunk.X != world.ChunksInRow - 1)
                {
                    setBottomAutoChunk(chunk, world, neighbor, x, y, step);
                }
                setRightAuto(chunk, world, neighbor, x, y, step);

                if (updateOnly == false) { setType(neighbor, roadState, (ushort)x, (ushort)y, chunk); }
            }
            #endregion
        }

        // Wyjaśnienie która storna jest która
        //
        //      Top /\  Left
        //         /  \
        //         \  /
        //    Right \/ Bottom
        //
        // Dodatkowo np: Top Left to czubek
        #region Set auto side
        /// <summary>
        /// Szukanie pobliskiego pola i sprawdzanie czy znajduje sie tam droga
        /// </summary>
        /// <param name="chunk">Chunk w którym znajduje się tworzona droga</param>
        /// <param name="neighbor">Tablica do której zwraca czy w pobliskim polu znajduje się doroga</param>
        /// <param name="x">X pola w chunku</param>
        /// <param name="y">Y pola w chunku</param>
        /// <param name="step">Czy zrobić update pobliskich dróg. True = nie, False = tak</param>
        private void setTopAuto(Chunk chunk, World world, bool[] neighbor, int x, int y, bool step)
        {
            if (chunk[(ushort)(x - 1), (ushort)(y)] == Block.Road.Id)
            {
                neighbor[0] = true;
                if (step == false)
                {
                    SetMetaAuto(world, chunk, x - 1, y, true);
                }
            }
            else
            {
                neighbor[0] = false;
            }
        }
        private void setLeftAuto(Chunk chunk, World world, bool[] neighbor, int x, int y, bool step)
        {

            if (chunk[(ushort)(x), (ushort)(y - 1)] == Block.Road.Id)
            {
                neighbor[1] = true;
                if (step == false)
                {
                    SetMetaAuto(world, chunk, (ushort)(x), (ushort)(y - 1), true);
                }
            }
            else
            {
                neighbor[1] = false;
            }
        }
        private void setBottomAuto(Chunk chunk, World world, bool[] neighbor, int x, int y, bool step)
        {
            if (chunk[(ushort)(x + 1), (ushort)(y)] == Block.Road.Id)
            {
                neighbor[2] = true;
                if (step == false)
                {
                    SetMetaAuto(world, chunk, (ushort)(x + 1), (ushort)(y), true);
                }
            }
            else
            {
                neighbor[2] = false;
            }
        }
        private void setRightAuto(Chunk chunk, World world, bool[] neighbor, int x, int y, bool step)
        {
            if (chunk[(ushort)(x), (ushort)(y + 1)] == Block.Road.Id)
            {
                neighbor[3] = true;
                if (step == false)
                {
                    SetMetaAuto(world, chunk, (ushort)(x), (ushort)(y + 1), true);
                }
            }
            else
            {
                neighbor[3] = false;
            }
        }
        #endregion

        #region Set auto chunk side
        /// <summary>
        /// Patrz: SetTopAuto
        /// </summary>
        private void setTopAutoChunk(Chunk chunk, World world, bool[] neighbor, int x, int y, bool step)
        {
            chunk = world.GetChunk(chunk.X - 1, chunk.Y);
            if (chunk[(ushort)(Chunk.CHUNK_SIZE - 1), (ushort)(y)] == Block.Road.Id)
            {
                neighbor[0] = true;
                if (step == false)
                {
                    SetMetaAuto(world, chunk, Chunk.CHUNK_SIZE - 1, y, true);
                }
            }
            else
            {
                neighbor[0] = false;
            }
        }
        private void setLeftAutoChunk(Chunk chunk, World world, bool[] neighbor, int x, int y, bool step)
        {
            chunk = world.GetChunk(chunk.X, chunk.Y - 1);
            if (chunk[(ushort)(x), (ushort)(Chunk.CHUNK_SIZE - 1)] == Block.Road.Id)
            {
                neighbor[1] = true;
                if (step == false)
                {
                    SetMetaAuto(world, chunk, x, Chunk.CHUNK_SIZE - 1, true);
                }
            }
            else
            {
                neighbor[1] = false;
            }
        }
        private void setBottomAutoChunk(Chunk chunk, World world, bool[] neighbor, int x, int y, bool step)
        {
            chunk = world.GetChunk(chunk.X + 1, chunk.Y);
            if (chunk[(ushort)(0), (ushort)(y)] == Block.Road.Id)
            {
                neighbor[2] = true;
                if (step == false)
                {
                    SetMetaAuto(world, chunk, 0, y, true);
                }
            }
            else
            {
                neighbor[2] = false;
            }
        }
        private void setRightAutoChunk(Chunk chunk, World world, bool[] neighbor, int x, int y, bool step)
        {
            chunk = world.GetChunk(chunk.X, chunk.Y + 1);
            if (chunk[(ushort)(x), (ushort)(0)] == Block.Road.Id)
            {
                neighbor[3] = true;
                if (step == false)
                {
                    SetMetaAuto(world, chunk, x, 0, true);
                }
            }
            else
            {
                neighbor[3] = false;
            }
        }
        #endregion

        #region Set type
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Tablica która zawiera informacje o tym czy w pobliskim polu znajduje się droga</param>
        /// <param name="state">Trawa?</param>
        /// <param name="x">X pola w chunku </param>
        /// <param name="y">Y pola w chunku</param>
        /// <param name="chunk">Chunk w którym ma zostać z updatowane pole</param>
        private void setType(bool[] n, int state, ushort x, ushort y, Chunk chunk)
        {
            #region Empty road
            if (n[0] == false && n[1] == false && n[2] == false && n[3] == false)
            {
                SetMeta((int)EnumRoadState.intersection, state, chunk, x, y);
            }
            #endregion
            #region One end
            else if (n[0] == true && n[1] == false && n[2] == false && n[3] == false)
            {
                SetMeta((int)EnumRoadState.end_right_bottom, state, chunk, x, y);
            }
            else if (n[0] == false && n[1] == true && n[2] == false && n[3] == false)
            {
                SetMeta((int)EnumRoadState.end_left_bottom, state, chunk, x, y);
            }
            else if (n[0] == false && n[1] == false && n[2] == true && n[3] == false)
            {
                SetMeta((int)EnumRoadState.end_left_top, state, chunk, x, y);
            }
            else if (n[0] == false && n[1] == false && n[2] == false && n[3] == true)
            {
                SetMeta((int)EnumRoadState.end_right_top, state, chunk, x, y);
            }
            #endregion
            #region Corners
            else if (n[0] == true && n[1] == true && n[2] == false && n[3] == false)
            {
                SetMeta((int)EnumRoadState.corner_top, state, chunk, x, y);
            }
            else if (n[0] == true && n[1] == false && n[2] == false && n[3] == true)
            {
                SetMeta((int)EnumRoadState.corner_left, state, chunk, x, y);
            }
            else if (n[0] == false && n[1] == true && n[2] == true && n[3] == false)
            {
                SetMeta((int)EnumRoadState.corner_right, state, chunk, x, y);
            }
            else if (n[0] == false && n[1] == false && n[2] == true && n[3] == true)
            {
                SetMeta((int)EnumRoadState.corner_bottom, state, chunk, x, y);
            }
            #endregion
            #region Straight
            else if (n[0] == true && n[1] == false && n[2] == true && n[3] == false)
            {
                SetMeta((int)EnumRoadState.straight_left, state, chunk, x, y);
            }
            else if (n[0] == false && n[1] == true && n[2] == false && n[3] == true)
            {
                SetMeta((int)EnumRoadState.straight_right, state, chunk, x, y);
            }
            #endregion
            #region Crossroads
            else if (n[0] == true && n[1] == true && n[2] == true && n[3] == false)
            {
                SetMeta((int)EnumRoadState.T_right_top, state, chunk, x, y);
            }
            else if (n[0] == true && n[1] == true && n[2] == false && n[3] == true)
            {
                SetMeta((int)EnumRoadState.T_left_top, state, chunk, x, y);
            }
            else if (n[0] == true && n[1] == false && n[2] == true && n[3] == true)
            {
                SetMeta((int)EnumRoadState.T_left_bottom, state, chunk, x, y);
            }
            else if (n[0] == false && n[1] == true && n[2] == true && n[3] == true)
            {
                SetMeta((int)EnumRoadState.T_right_bottom, state, chunk, x, y);
            }
            #endregion
            #region Intersection
            else if (n[0] == true && n[1] == true && n[2] == true && n[3] == true)
            {
                SetMeta((int)EnumRoadState.intersection, state, chunk, x, y);
            }
            #endregion
        }
        #endregion
    }

}
