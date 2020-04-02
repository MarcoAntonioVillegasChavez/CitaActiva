using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class AgenciaCita
    {
        public int id_agencia_cita { get; set; }
        public int id_cita { get; set; }
        public int id_agencia { get; set; }
        public string agencia { get; set; }
        public string google_place { get; set; }
        public  int id_zona { get; set; }
        public virtual Citas Citas { get; set; }
    }
}
