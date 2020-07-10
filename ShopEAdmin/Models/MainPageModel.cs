using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEAdmin.Models
{
    public class MainPageModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<Commodity> Commodities { get; set; }
        public List<Payment> Payments { get; set; }
        public long PaymentTotal { get; set; }
        public int PaymentCount { get; set; }
    }
}
