using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("clientes")]
    public class Clientes
    {
        [Key]
        public int id_cliente { get; set; }
        public string cuenta_personal { get; set; }
        public string nombre_cliente { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime fecha_nacimiento { get; set; }
        public string email_cliente { get; set; }
        public string telefono { get; set; }
        public string password { get; set; }
        public string rfc { get; set; }
        public string homo_clave { get; set; }
        public DateTime fecha_registro { get; set; }
        public int cliente_activo { get; set; }
    }
}
