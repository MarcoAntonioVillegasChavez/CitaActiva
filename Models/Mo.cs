using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Mo")]
    public class Mo
    {
        [Key]
        public int id_mo { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}
