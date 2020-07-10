using System;
using System.Collections.Generic;
using System.Text;

namespace ShopE.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public virtual List<Commodity> Commodities { get; set; }

    }
}
