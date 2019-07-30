using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Kits_articulos")]
    public class KitsArticulos
    {
        [Key, ForeignKey("Kits")]
        public int id_kit { get; set; }
        [Key, ForeignKey("Articulos")]
        public int id_articulo { get; set; }
        public decimal precio { get; set; }
        public decimal descuento { get; set; }
    }
}
