using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopE.Server.Models
{
    public class Commodity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public float Off { get; set; }
        public int TotalPrice { get; set; }
        public int Count { get; set; }
        public string Photo { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

    }
}
