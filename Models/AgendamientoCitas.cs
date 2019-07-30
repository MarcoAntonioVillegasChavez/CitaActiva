using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("agendamiento_citas")]
    public class AgendamientoCitas
    {
        [Key]
        public int idagendamiento_citas { get; set; }
        [ForeignKey("Citas")]
        public int id_cita { get; set; }
        [ForeignKey("Agencias")]
        public int id_agencia { get; set; }
        public string planned_date { get; set; }
        public string planned_time { get; set; }
    }
}
