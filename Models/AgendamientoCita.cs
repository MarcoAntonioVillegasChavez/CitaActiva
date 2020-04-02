using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class AgendamientoCita
    {
        public int id_agendamiento_cita { get; set; }
        public int id_cita { get; set; }
        public DateTime planned_date { get; set; }
        public TimeSpan planned_time { get; set; }
        public int active_ind { get; set; }

        public virtual Citas Citas { get; set; }
    }
}
