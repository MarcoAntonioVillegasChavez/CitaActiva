using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Workshops")]
    public class Workshop
    {
        public int id { get; set; }
        public string name { get; set; }
        public string fullName { get; set; }
        public string comercialName { get; set; }
        public string workshopCode { get; set; }
        //public string delegationName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string phoneNumber { get; set; }
        public string documentId { get; set; }
        //public string text { get; set; }
        //public string receptionistLabel { get; set; }
        //public string fax { get; set; }
        public string email { get; set; }
        public int companyId { get; set; }
        public int warehouseId { get; set; }
        public string brandId { get; set; }
        //public Boolean showBrandVehicles { get; set; }
        //public string showSendEmail { get; set; }
        //public Boolean activeAppointment { get; set; }
        //public string firstDate { get; set; }
        //public string gpsLongitude { get; set; }
        //public string gpsLatitude { get; set; }
        //public string brandsVisualInspection { get; set; }

        public int activeInd { get; set; }
    }
}
