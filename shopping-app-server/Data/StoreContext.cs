using API.Entities;
using API.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
       
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Unit> Units { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Tax> Tax { get; set; }
        public DbSet<SalesHeader> SalesHeader { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }

    }
}
