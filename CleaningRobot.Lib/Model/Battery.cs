using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot.Lib
{
    public class Battery
    {
        public int Status { get; set; }
        public int Consumption(Action action) => batteryConsumption[action];
        private Dictionary<Action, int> batteryConsumption { get; set; } = new Dictionary<Action, int>();
        public Battery()
        {
            batteryConsumption.Add(Action.TL, 1);
            batteryConsumption.Add(Action.TR, 1);
            batteryConsumption.Add(Action.A, 2);
            batteryConsumption.Add(Action.B, 3);
            batteryConsumption.Add(Action.C, 5);
        }
    }
}
