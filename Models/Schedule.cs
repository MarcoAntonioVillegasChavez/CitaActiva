using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Schedule
    {
        public string id { get; set; }
        public string description { get; set; }
        public List<Days> days { get; set; }
    }
    public class Days
    {
        public string weekday { get; set; }
        public string beginning { get; set; }
        public string ending { set; get; }
    }
}
