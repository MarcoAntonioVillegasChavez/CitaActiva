using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Error
    {
        public int id_error { get; set; }
        public string detalle { get; set; }
        public DateTime fecha { get; set; }
    }
}
