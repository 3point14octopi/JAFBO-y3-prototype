using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Generation_Tools
{
    public static class Basic {
        public static void Swap<T>(T a, T b)
        {
            T temp = b;
            b = a;
            a = temp;
        }
        public static float PercentageOf(Space2D space, Cell tileType)
        {
            int tCount = 0;
            for (int i = 0; i < space.height; i++)
            {
                for (int j = 0; j < space.width; j++)
                {
                    if (space.GetCell(new Coord(j, i)) == tileType.value)
                    {
                        tCount++;
                    }
                }
            }

            return ((float)tCount / space.area());
        }
    }




    public static class RNG {
        public static int GenRand(int min, int range)
        {
            return UnityEngine.Random.Range(min, (min + range) - 1);
        }
        public static Coord GenRandCoord(Space2D space, bool includeBorder = false)
        {
            int inclusive = (!includeBorder) ? 1 : 0;

            return new Coord((GenRand(inclusive, space.width - (inclusive * 2))), (GenRand(inclusive, space.height - (inclusive * 2))));

        }
        public static T CircleSelect<T>(List<T> coords, int start)
        {
            if(start < coords.Count)
            {
                int x = GenRand(start, coords.Count - 1);
                Basic.Swap(coords[start], coords[x]);
            }
            else
            {
                start = coords.Count - 1;
            }

            return coords[start];
        }

    }
      
}
