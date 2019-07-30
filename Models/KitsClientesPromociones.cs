using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Kits_clientes_promociones")]
    public class KitsClientesPromociones
    {
        [Key, ForeignKey("KitsClientes")]
        public int idkit_cliente { get; set; }
        [Key, ForeignKey("Promociones")]
        public int id_promocion { get; set; }
    }
}
