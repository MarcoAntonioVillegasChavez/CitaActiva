using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    //[Table("servicios")]
    public class Servicios
    {
        [Key]
        public int id_servicio { get; set; }
        public int  kilometraje_maximo { get; set; }
        public int anho { get; set; }
        public int id_familia { get; set; }
        public double precio { get; set; }
        public int id_combustible { get; set; }
        public string codigo_qis { get; set; }
    }
}
