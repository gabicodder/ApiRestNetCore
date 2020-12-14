using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiREST.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string producto { get; set; }
        public decimal valor { get; set; }
        public int unidad { get; set; }

    }
}
