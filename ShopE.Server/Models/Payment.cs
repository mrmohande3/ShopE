using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopE.Server.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int CommodityId { get; set; }
        public virtual Commodity Commodity { get; set; }
        public string CustomerName { get; set; }
        public long TotalPrice { get; set; }
        public string CustomerPhoneNumber { get; set; }

    }
}
