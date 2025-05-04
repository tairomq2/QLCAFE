using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductPrice
    {
        [Key]
        public int PriceID { get; set; }
        public int ProductID { get; set; }
        public decimal ImportPrice { get; set; }
        public DateTime EffectiveDate { get; set; } = DateTime.Now;
        public Product? Product { get; set; }
    }
}
