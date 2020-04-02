using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class KitCita
    {
        public int id_kit_cita { get; set; }
        public int id_cita { get; set; }
        public int id_kit { get; set; }
        public string codigo_qis { get; set; }
        public string kit { get; set; }
        public decimal costo { get; set; }

        public virtual Citas Citas { get; set; }

    }
}
