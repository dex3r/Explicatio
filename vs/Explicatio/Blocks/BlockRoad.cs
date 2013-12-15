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
        public void SetMetaAuto(World world, Chunk chunk, ushort x, ushort y, bool step = false)
        {
            int roadState = 0;
            bool[] neighbor = new bool[4];
            Chunk neighborChunk;
            #region Middle
            if (x != 0 && x != Chunk.CHUNK_SIZE - 1 && y != 0 && y != Chunk.CHUNK_SIZE - 1)
            {
                setTopAuto(chunk, world, neighbor, x, y, step);
                setLeftAuto(chunk, world, neighbor, x, y, step);
                setBottomAuto(chunk, world, neighbor, x, y, step);
                setRightAuto(chunk, world, neighbor, x, y, step);

                setType(neighbor, roadState, x, y, chunk);
            }
            #endregion

        }

        private void setTopAuto(Chunk chunk, World world, bool[] neighbor, ushort x, ushort y, bool step)
        {
            if (chunk[(ushort)(x - 1), (ushort)(y)] == Block.Road.Id)
            {
                neighbor[0] = true;
                if (step == false)
                {
                    SetMetaAuto(world, chunk, (ushort)(x - 1), (ushort)(y), true);
                }
            }
            else
            {
                neighbor[0] = false;
            }
        }
        private void setLeftAuto(Chunk chunk, World world, bool[] neighbor, ushort x, ushort y, bool step)
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
        private void setBottomAuto(Chunk chunk, World world, bool[] neighbor, ushort x, ushort y, bool step)
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
        private void setRightAuto(Chunk chunk, World world, bool[] neighbor, ushort x, ushort y, bool step)
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


        private void setType(bool[] n, int state, ushort x, ushort y, Chunk chunk)
        {
            //Empty road
            if (n[0] == false && n[1] == false && n[2] == false && n[3] == false)
            {
                SetMeta((int)EnumRoadState.intersection, state, chunk, x, y);
            }
            //One end
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
            //Corners
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
            //Straight
            else if (n[0] == true && n[1] == false && n[2] == true && n[3] == false)
            {
                SetMeta((int)EnumRoadState.straight_left, state, chunk, x, y);
            }
            else if (n[0] == false && n[1] == true && n[2] == false && n[3] == true)
            {
                SetMeta((int)EnumRoadState.straight_right, state, chunk, x, y);
            }
            //Crossroads
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
            //Intersection
            else if (n[0] == true && n[1] == true && n[2] == true && n[3] == true)
            {
                SetMeta((int)EnumRoadState.intersection, state, chunk, x, y);
            }
        }
    }

}
