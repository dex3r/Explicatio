using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Explicatio.Worlds;

namespace Explicatio.Blocks
{
    public static class BuildingContainer
    {
        public static SortedList<BuildingEnum, Building> container = new SortedList<BuildingEnum, Building>();

        public static void AddBuilding(World world, int chunkX, int chunkY, ushort x, ushort y, BuildingEnum building)
        {
            container.Add(building,new Building(new Point(chunkX,chunkY),x,y);
            world.GetChunk(chunkX, chunkY)[x, y] = (byte)building;
        }
    }
}
