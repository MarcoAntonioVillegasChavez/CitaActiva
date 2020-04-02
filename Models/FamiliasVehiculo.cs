using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    //[Table("familias_vehiculo")]
    public class FamiliasVehiculo
    {
        [Key]
        public int id_familia { get; set; }
        public string nombre_familia { get; set; }
        public int id_marca { get; set; }
        public string codigo_qis { get; set; }
        
    }
}
