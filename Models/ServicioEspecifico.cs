using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class ServicioEspecifico
    {
        public int id_especifico { get; set; }
        public string codigo_qis { get; set; }
        public string descripcion { get; set; }
        public decimal costo { get; set; }
    }
}
