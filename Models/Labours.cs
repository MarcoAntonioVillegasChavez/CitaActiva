using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
   
    public class Labours
    {
        public int id { get; set; }
        public string description { get; set; }
        public int? plannedHours { get; set; }
        public string operatorId { get; set; }
        public string teamId { get; set; }
        public int? tipo { get; set; }
    }
}
