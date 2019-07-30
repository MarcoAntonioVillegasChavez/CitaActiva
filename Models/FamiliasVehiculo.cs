using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("familias_vehiculo")]
    public class FamiliasVehiculo
    {
        [Key]
        public int id_familiavehiculo { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        [ForeignKey("MarcasVehiculo")]
        public int id_marcavehiculo { get; set; }
        public int id_tipovehiculo { get; set; }
    }
}
