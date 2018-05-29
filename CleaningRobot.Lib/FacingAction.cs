using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot.Lib
{
    public class FacingAction
    {
        public static string[][] Result { get; set; } = new string[][] {
                                                                        new string[2] { "W", "E" },
                                                                        new string[2] { "N", "S" },
                                                                        new string[2] { "E", "W" },
                                                                        new string[2] { "S", "N" } };
    }
}
