using RepairShop.Models;
using System.Data.Entity;

namespace RepairShop.DAL
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext() 
            : base("DefaultConnection")
        {

        }

        public DbSet<RepairOrder> RepairOrders { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Part> Parts { get; set; }
    }
}