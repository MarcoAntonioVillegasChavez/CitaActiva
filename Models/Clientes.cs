using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Clientes")]
    public class Clientes
    {
        public int clienteId { get; set; }
        public string cuenta_personal { get; set; }
        public string nombre_cliente { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string email_cliente { get; set; }
        public string fecha_nacimiento { get; set; }
        public string password { get; set; }
        public string rfc { get; set; }
        public string fecha_registro { get; set; }
    }
}
