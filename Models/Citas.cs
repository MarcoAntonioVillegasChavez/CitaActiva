using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Citas
    {
        public int id_cita { get; set; }
        public string id_appointment { get; set; }
        public int id_cliente { get; set; }
        public int id_invitado { get; set; }
        public int status_cita { get; set; }
        public DateTime fecha_registro { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public DateTime fecha_cancelacion { get; set; }

        public virtual Clientes Clientes { get; set; }
        public virtual Invitados Invitados { get; set; }

        public virtual ICollection<AgenciaCita> AgenciaCitas { get; set; }
        public virtual ICollection<AgendamientoCita> AgendamientoCitas { get; set; }
        public virtual ICollection<ArticuloCita> ArticuloCitas { get; set; }
        public virtual ICollection<KitCita> KitCitas { get; set; }
        public virtual ICollection<MoCita> MoCitas { get; set; }
    }
    public class Cita
    {
        public int id_cita { get; set; }
        public string id { get; set; }
        public string id_agencia { get; set; }
        public string necesita_carro_reemplazo { get; set; }
        public string placa_vehiculo { get; set; }
        public string nombre_contacto { get; set; }
        public bool recive_cliente { get; set; }
        public string email { get; set; }
        public bool vehiculo_pickup { get; set; }
        public string telefono { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public int kilometraje { get; set; }
        public string id_recepcionista { get; set; }
        public string comentarios { get; set; }
        public DateTime fecha_creacion { get; set; }
    }
}
