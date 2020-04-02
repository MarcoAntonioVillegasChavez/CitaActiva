using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class ArticuloCita
    {
        public int id_articulo_cita { get; set; }
        public int id_cita { get; set; }
        public int id_articulo { get; set; }
        public string codigo_qis { get; set; }
        public string descripcion { get; set; }

        public virtual Citas Citas { get; set; }
    }
}
