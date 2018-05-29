using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot.Lib
{
    public class Rules
    {
        public static string[] Commands = new string[] { "TR", "TL", "A", "B", "C" };
        private static string[] Maps = new string[] { "S", "C", "null" };

        public static bool CheckMap(string[][] map)
        {
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] != null)
                {
                    for (int j = 0; j < map[i].Length; j++)
                    {
                        if (!Maps.Contains(map[i][j]) || map[i][j] == null)
                            return false;
                    }
                }
                else return false;
            }
            return true;
        }
    }
}
