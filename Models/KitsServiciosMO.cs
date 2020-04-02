using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class KitsServiciosMO
    {
        public int id_kit_servicio { get; set; }
        public int id_mano_obra { get; set; }
        public virtual KitServicio kit_servicio { get; set; }
        public virtual Mo mano_obra { get; set; }
    }
}
