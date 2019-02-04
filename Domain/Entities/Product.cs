using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    public class Product : Entity<int>
    {

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double PriceToBuy { get; set; }




    }
}
