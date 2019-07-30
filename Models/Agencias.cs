using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("agencias")]
    public class Agencias
    {
        [Key]
        public int id_agencia { get; set; }
        [ForeignKey("id_zona")]
        public int id_zona { get; set; }
        public string nombre_agencia { get; set; }
        public int active_ind { get; set; }
        public string place_id { get; set; }

    }
}
