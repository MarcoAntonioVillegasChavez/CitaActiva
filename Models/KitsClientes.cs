using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Kits_Clientes")]
    public class KitsClientes
    {
        [Key]
        public int idkit_cliente { get; set; }
        [ForeignKey("Kits")]
        public int id_kit { get; set; }
        [ForeignKey("Clientes")]
        public int id_cliente { get; set; }
        public int idvehiculo_cliente { get; set; }
    }
}
