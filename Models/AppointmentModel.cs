using CitaActiva.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.ModelsViews
{
    public class AppointmentModel
    {
        public string id { get; set; }
        public Boolean needReplacementCar { get; set; }
        public string vehiclePlate { get; set; }
        public string contactName  { get; set; }
        public Boolean clientReceived { get; set; }
        public int workshopId { get; set; }
        public string contactMail { get; set; }
        public Boolean pickUpVehicle { get; set; }
        public string vehicleYear { get; set; }        
        //public string brandId { get; set; }
        public string versionId { get; set; }
        public string contactPhone { get; set; }
        public PlannedData plannedData { get; set; }
        public Labours labours { get; set; }

    }
}
