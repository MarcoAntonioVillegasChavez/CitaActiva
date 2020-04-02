using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Articulos
    {
        [Key]
        public int id_articulo { get; set; }
        public string codigo_qis { get; set; }
        public string descripcion { get; set; }
        public double precio { get; set; } 
        public double descuento { get; set; }
    }
}
