using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleaningRobot.WebAPI.Models
{
    public class RequestModel
    {
        public string[][] Map { get; set; }
        public Object Start { get; set; }
        public string[] Commands { get; set; }
        public int Battery { get; set; }
    }
}