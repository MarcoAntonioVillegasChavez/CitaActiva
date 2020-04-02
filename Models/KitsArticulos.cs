﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Models
{
    public class KitsArticulos
    {
        public int id_kit { get; set; }
        public int id_articulo { get; set; }
        public decimal precio { get; set; }
        public decimal descuento { get; set; }
    }
}