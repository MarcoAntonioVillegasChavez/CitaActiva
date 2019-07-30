using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
      
    public class Appointment
    {
        public string id { get; set; }
        public string contactName { get; set; }
        public string contactMail { get; set; }
        public string contactPhone { get; set; }
        public string cuentaPersonal { get; set; }
        public string brandId { get; set; }
        public string brandCode { get; set; }
        public string versionId { get; set; }
        public string version { get; set; }
        public int vehicleYear { get; set; }
        public string vehiclePlate { get; set; }
        public int workshopId { get; set; }
        public PlannedData plannedData { get; set; }
        public Labours labours { get; set; }
        public int invitadoInd { get; set; }
       
    }
}
