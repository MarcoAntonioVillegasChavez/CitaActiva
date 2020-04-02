using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class KitServiciosRefacciones
    {
        public int id_kit_servicio { get; set; }
        public int id_refaccion { get; set; }

        public virtual KitServicio kit_servicio { get; set; }
        public virtual Articulos refaccion { get; set; }
    }
}
