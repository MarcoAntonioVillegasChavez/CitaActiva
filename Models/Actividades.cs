using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Actividades
    {
        public int id_actividad { get; set; }
        public string actividades { get; set; }
        public string descripcion { get; set; }
        public int activo { get; set; }
        
        public virtual ICollection<KitConceptoActividad> kitConceptoActividad { get; set; }
    }
}
