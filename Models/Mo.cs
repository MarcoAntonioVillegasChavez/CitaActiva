using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Mo
    {
        [Key]
        public int id_mano_obra { get; set; }
        public string codigo_qis { get; set; }
        public string descripcion { get; set; }
        public double precio { get; set; }
        public double descuento { get; set; }

    }
}
