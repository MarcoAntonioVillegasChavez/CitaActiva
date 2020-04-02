using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Descuentos")]
    public class Descuentos
    {
        [Key]
        public int id_descuento { get; set; }
        public string descripcion { get; set; }
        public decimal porcentajeDescuento { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_vigencia { get; set; }
    }
}
