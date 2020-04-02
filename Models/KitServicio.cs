using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class KitServicio
    {
        public int id_kit_servicio { get; set; }
        public int id_kit { get; set; }
        public string descripcion { get; set; }
        public string urlImg1 { get; set; }
        public string urlImg2 { get; set; }

        public List<Mo> Mo { get; set; }
        public List<Articulos> Articulos { get; set; }
    }
}
