using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleaningRobot.WebAPI.Models
{
    public class ResultModel
    {
        public ResultModel()
        {
            Visited = new List<Object>();
            Cleaned = new List<Object>();
        }

        [JsonProperty("visited")]
        public List<Object> Visited { get; set; }
        [JsonProperty("cleaned")]
        public List<Object> Cleaned { get; set; }
        [JsonProperty("final")]
        public Object Final { get; set; }
        [JsonProperty("battery")]
        public int Battery { get; set; }
    }
}