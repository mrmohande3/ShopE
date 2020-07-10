using System;
using System.Collections.Generic;
using System.Text;

namespace ShopE.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
