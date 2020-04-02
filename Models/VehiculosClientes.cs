using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("vehiculos_clientes")]
    public class VehiculosClientes
    {
        [Key]
        public int idvehiculo_cliente { get; set; }
        public int id_cliente { get; set; }
        [ForeignKey("FamiliasVehiculo")]
        public int id_familiavehiculo { get; set; }
        public int kilometraje { get; set; }
        public int anhio { get; set; }
        public string placa { get; set; }
        public string vin { get; set; }
        [ForeignKey("TipoCombustible")]
        public int id_tipocombustible { get; set; }

    }
}
