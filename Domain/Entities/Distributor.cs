using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Distributor : Entity<int>
    {

        [Required]
        public string Nit;
        [Required]
        public string Name;
        [Required]
        public double Credit;
        [Required]
        public string direcction;
  
        public List<Venta> Ventas;
    }
}
