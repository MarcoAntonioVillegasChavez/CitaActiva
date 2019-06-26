using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    [Table("Token")]
    public class Token
    {
        [Key]
        public int id { get; set; }
        public string token_type { get; set; }
        public string access_token { get; set; }
        public string expires_in { get; set; }    
        public DateTime createdAt { get; set; }
    }
}
