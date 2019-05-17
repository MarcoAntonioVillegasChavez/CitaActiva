using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Receptionist
    {
        public string id { get; set; }
        public string name { get; set; }
        public string account { get; set; }
        public string workshopId { get; set; }
        public string scheduleId { get; set; }
        public string time { get; set; }
        public string operatorId { get; set; }
        public string active { get; set; }
        public List<string> email { get; set; }
    }
}

