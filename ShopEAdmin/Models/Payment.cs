using System;
using System.Collections.Generic;
using System.Text;

namespace ShopEAdmin.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int CommodityId { get; set; }
        public Commodity Commodity { get; set; }
        public string CustomerName { get; set; }
        public long TotalPrice { get; set; }
        public string CustomerPhoneNumber { get; set; }
    }
}
