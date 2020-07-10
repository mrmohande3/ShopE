using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopE.Server.Models
{
    public class ShopEContext : DbContext
    {
            public ShopEContext(DbContextOptions<ShopEContext> options) : base(options)
            {

            }
            public DbSet<Store> Stores { get; set; }
            public DbSet<Commodity> Commodities { get; set; }
            public DbSet<Payment> Payments { get; set; }
    }
}
