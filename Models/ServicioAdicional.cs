using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class ServicioAdicional
    {
        public int id_adicional { get; set; }
        public string codigo_qis { get; set; }
        public string descripcion { get; set; }
        public decimal costo { get; set; }  
        public int id_servicio_adicional { get; set; }
        public string servicio_adicional_desc { get; set; }
    }
}
