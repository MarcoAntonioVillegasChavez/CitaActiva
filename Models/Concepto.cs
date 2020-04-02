using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Concepto
    {
        public int id_concepto { get; set; }
        public string concepto { get; set; }
        public string descripcion { get; set; }
        public int tipo { get; set; }
        public int id_kit_concepto { get; set; }

        public ICollection <KitConcepto> kitConcepto { get; set; }
    }
}
