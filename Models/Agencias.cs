using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class Agencias
    {
        public int id_agencia { get; set; }
        public int id_zona { get; set; }
        public string nombre_agencia { get; set; }
        public bool activa { get; set; }
        public string codigo_quis { get; set; }
        public string google_place { get; set; }
        public string place_id { get; set; }
    }
}
