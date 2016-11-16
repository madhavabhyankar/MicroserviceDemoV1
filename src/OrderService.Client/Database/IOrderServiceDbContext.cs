using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Client.Database
{
    public interface IOrderServiceDbContext
    {
        DbSet<Order> Orders { get; set; }
       Task<int> SaveChangesAsync();
    }
}
