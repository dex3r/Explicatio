using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Explicatio.Blocks;
using System.Reflection;

namespace Explicatio.Textures
{
    public static class TexturesRoads
    {
        public static Texture2D[][] Textures;

        public static void Load(ContentManager cm)
        {
            Textures = new Texture2D[Enum.GetValues(typeof(EnumRoadType)).Length][];
            Array types = Enum.GetValues(typeof(EnumRoadType));
            Array states = Enum.GetValues(typeof(EnumRoadState));
           for(int i = 0; i < types.Length; i++)
           {
               Textures[i] = new Texture2D[Enum.GetValues(typeof(EnumRoadState)).Length];
               for(int j = 0; j < states.Length; j++)
               {
                   Textures[i][j] = cm.Load<Texture2D>(@"gfx\roads\" + types.Cast<EnumRoadType>().ToArray<EnumRoadType>()[i] + @"\" + states.Cast<EnumRoadState>().ToArray<EnumRoadState>()[j]);
               }
           }
        }

        public static Texture2D GetTexture(EnumRoadType roadType, EnumRoadState state)
        {
            return GetTexture((int)roadType, (int)state);
        }

        public static Texture2D GetTexture(int roadType, EnumRoadState state)
        {
            return GetTexture(roadType, (int)state);
        }

        public static Texture2D GetTexture(EnumRoadType roadType, int state)
        {
            return GetTexture((int)roadType, state);
        }

        public static Texture2D GetTexture(int roadType, int state)
        {
            return Textures[roadType][state];
        }
    }
}
