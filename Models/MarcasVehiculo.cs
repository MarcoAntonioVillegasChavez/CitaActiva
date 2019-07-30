using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("marcas_vehiculo")]
    public class MarcasVehiculo
    {
        [Key]
        public int id_marcavehiculo { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}
