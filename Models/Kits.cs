using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Kits
    {
        [Key]
        public int id_kit { get; set; }
        public string codigo_qis { get; set; }
        public string descripcion { get; set; }
        public string uri_foto { get; set; }
        public string color { get; set; }
        public List<Articulos> articulos { get; set; }
        public List<Mo> mos { get; set; }
        public List<Zonas> zonas { get; set; }
        public List<KitServicio> kitServicios { get; set; }
        public List<Concepto> conceptos { get; set; }
    }
}
