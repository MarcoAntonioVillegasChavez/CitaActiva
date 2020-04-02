using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class KitConceptoActividad
    {
        public int id_kit_concepto_actividad { get; set; }
        public int id_kit_concepto { get; set; }
        public int id_actividad { get; set; }
        public int activo { get; set; }

        public virtual Actividades actividades { get; set; }
        public virtual KitConcepto kitConcepto { get; set; }
    }
}
