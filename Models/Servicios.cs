using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("servicios")]
    public class Servicios
    {
        [Key]
        public int id_servicio { get; set; }
        public string kilometraje { get; set; }
        public int anho { get; set; }
        [ForeignKey("FamiliasVehiculo")]
        public int id_familiavehiculo { get; set; }
        public float precio { get; set; }
        [ForeignKey("tipo_combustible")]
        public int id_tipocombustible { get; set; }
    }
}
