using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopE.Server.Models;

namespace ShopE.Server
{
    public interface IDBInitializer
    {
        void InitializeDb();
    }
    public class DBInitializer : IDBInitializer
    {
        private readonly ShopEContext _context;

        public DBInitializer(ShopEContext context)
        {
            _context = context;
        }

        public void InitializeDb()
        {
            /*_context.Database.EnsureCreatedAsync();
            _context.Database.Migrate();*/
        }
    }
}
