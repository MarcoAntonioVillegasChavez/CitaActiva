using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("tipo_combustible")]
    public class TipoCombustible
    {
        [Key]
        public int id_tipocombustible { get; set; }
        public string descripcion { get; set; }
    }
}
