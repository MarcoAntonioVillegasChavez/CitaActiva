using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Articulos")]
    public class Articulos
    {
        [Key]
        public int id_articulo { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }       
    }
}
