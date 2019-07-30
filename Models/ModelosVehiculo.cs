using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Modelos_vehiculo")]
    public class ModelosVehiculo
    {
        [Key]
        public int id_modelovehiculo { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        [ForeignKey("FamiliasVehiculo")]
        public int id_familiavehiculo { get; set; }
    }
}
