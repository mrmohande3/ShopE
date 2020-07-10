using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEAdmin.Models
{
    public class Commodity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string Photo { get; set; }
        public int StoreId { get; set; }
        public float Off { get; set; }
        public virtual Store Store { get; set; }
        public float PriceWOff
        {
            get
            {
                return (Price) - ((Price / 100) * Off);
            }
        }
    }
}
