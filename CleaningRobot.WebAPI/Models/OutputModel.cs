using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleaningRobot.WebAPI.Models
{
    public class OutputModel
    {
        public string status { get; set; }
        public object data { get; set; }
        public string message { get; set; }
    }
}