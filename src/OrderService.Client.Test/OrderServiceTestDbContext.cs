using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderService.Client.Database;
using TestWithEF;

namespace OrderService.Client.Test
{
    public class OrderServiceTestDbContext : IOrderServiceDbContext
    {
        public DbSet<Order> Orders { get; set; }  = new TestDbSet<Order>();
        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }
    }
}
