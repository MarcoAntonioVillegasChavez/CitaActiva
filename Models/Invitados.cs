using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Invitados
    {
        public int id_invitado { get; set; }
        public string nombre_cliente { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string email_cliente { get; set; }
        public string telefono { get; set; }
        public string rfc { get; set; }

        public virtual ICollection<Citas> Citas { get; set; }
    }
}
