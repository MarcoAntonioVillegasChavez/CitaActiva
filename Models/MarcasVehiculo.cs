using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    //[Table("marcas_vehiculo")]
    public class MarcasVehiculo
    {
        [Key]
        public int id_marca { get; set; }
        public string codigo_qis { get; set; }
        public string nombre_marca { get; set; }
    }
}
