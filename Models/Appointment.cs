using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Appointment
    {
        public string id { get; set; }
        public int workshopId { get; set; }
        public string comments { get; set; }
        public string vehiclePlate { get; set; }
        public Boolean mileage { get; set; }
        public string contactName { get; set; }
        public string contactPhone { get; set; }
        public string contactMail { get; set; }
        public Boolean clientReceived { get; set; }
        public Boolean pickUpVehicle { get; set; }
        public Boolean needReplacementCar { get; set; }
        public string idvReplacementCar { get; set; }
        public string initalDateReplacementCar { get; set; }
        public string finalDateReplacementCar { get; set; }
    }
}
