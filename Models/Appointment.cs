using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{

    [Table("Appointment")]
    public class Appointment
    {
        public string id { get; set; }
        public string contactName { get; set; }
        public string contactMail { get; set; }
        public string contactPhone { get; set; }
        public string brandId { get; set; }
        public string versionId { get; set; }
        public string version { get; set; }
        public int vehicleYear { get; set; }
        public string vehiclePlate { get; set; }
        public string labours { get; set; }
        public string laboursId { get; set; }
        public int workshopId { get; set; }
        public string plannedDate { get; set; }
        public string plannedTime { get; set; }       
    }
}
