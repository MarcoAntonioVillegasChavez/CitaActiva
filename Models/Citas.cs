using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Citas")]
    public class Citas
    {
        [Key]
        public int id_cita { get; set; }
        public string id_appointment { get; set; }
        public int idkit_cliente { get; set; }
        //[ForeignKey("Clientes")]
        //public int id_cliente { get; set; }
        public int status_cita { get; set; }
        public DateTime fecha_registro { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public DateTime fecha_cancelacion { get; set; }
        [ForeignKey("Servicios")]
        public int id_servicio { get; set; }
    }
}
