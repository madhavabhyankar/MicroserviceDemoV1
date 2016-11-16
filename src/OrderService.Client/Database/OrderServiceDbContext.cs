using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Client.Database
{
    public class OrderServiceDbContext : DbContext, IOrderServiceDbContext
    {
        public OrderServiceDbContext():
            base("orderServiceConnectionString")
        {
            
        }

        public DbSet<Order> Orders { get; set; }
    }
}
