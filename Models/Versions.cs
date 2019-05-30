using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Versions")]
    public class Versions
    {
        public string id { get; set; }
        public string description { get; set; }
        public string brandId { get; set; }
    }
}
