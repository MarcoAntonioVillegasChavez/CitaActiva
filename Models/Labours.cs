using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Labours
    {
        public string description { get; set; }
        public string id { get; set; }
        public int plannedHours { get; set; }
        public string operatorId { get; set; }
        public string teamId { get; set; }
    }
}
