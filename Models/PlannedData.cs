using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class PlannedData
    {
        public string plannedDate { get; set; } //"2019-04-15"
        public string plannedTime { get; set; } //"11:40:00"
        public int receptionistId { get; set; } // 110
    }
}
