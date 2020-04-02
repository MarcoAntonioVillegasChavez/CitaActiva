using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class KitConcepto
    {
        public int id_kit_concepto { get; set; }
        public int id_kit { get; set; }
        public int id_concepto { get; set; }

        public List<Actividades> actividades { get; set; }

        public virtual Kits kits { get; set; }
        public virtual Concepto concepto { get; set; }

    }
}
