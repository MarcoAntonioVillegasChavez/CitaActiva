using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Kits")]
    public class Kits
    {
        [Key]
        public int id_kit { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        [ForeignKey("Zonas")]
        public int id_zona { get; set; }
    }
}
