﻿using System;
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
        public BlockRoad(byte id, String name) : base(id, name)
        {
           
        }

        public override Texture2D GetTexture(Chunk c, ushort chunkX, ushort chunkY)
        {
            return TexturesRoads.GetTexture((0xF & c.GetMeta(chunkX, chunkY)), ((0xF0 & c.GetMeta(chunkX, chunkY)) >> 4));
        }

        public void SetMeta(int roadState, int roadType, int x, int y)
        {
            GameMain.CurrentWorld.SetMeta((UInt16)(roadType | (roadState << 4)) , x, y);
        }

        public void SetMeta(int roadState, int roadType, Chunk chunk, ushort x, ushort y)
        {
            chunk.SetMeta((UInt16)(roadType | (roadState << 4)), x, y);
        }
    }
}
