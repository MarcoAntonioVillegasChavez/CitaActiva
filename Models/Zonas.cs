using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("zonas")]
    public class Zonas
    {
        [Key]
        public int id_zona { get; set; }
        public string nombre_zona { get; set; }
    }
}
