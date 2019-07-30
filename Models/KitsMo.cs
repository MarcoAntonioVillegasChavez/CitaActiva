using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("kits_mo")]
    public class KitsMo
    {
        [Key, ForeignKey("Kits")]
        public int id_kit { get; set; }
        [Key, ForeignKey("Mo")]
        public int id_mo { get; set; }
        public decimal precio { get; set; }
        public decimal descuento { get; set; }
    }
}
