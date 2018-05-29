using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot.Lib
{
    public class AlternativeActions
    {
        public static string[][] Path { get; set; } = new string[][] {
                                                                        new string[] { "TR", "A" },
                                                                        new string[] { "TL", "B", "TR", "A" },
                                                                        new string[] { "TL", "TL", "A" },
                                                                        new string[] { "TR", "B", "TR", "A" },
                                                                        new string[] { "TL", "TL", "A" } };
    }
}
