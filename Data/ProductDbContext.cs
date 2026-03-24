using Microsoft.EntityFrameworkCore;
using ProductInventoryManagement.Models;

namespace ProductInventoryManagement.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAccount>().HasData(
                new UserAccount { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
                new UserAccount { Id = 2, Username = "manager", Password = "manager123", Role = "Manager" },
                new UserAccount { Id = 3, Username = "viewer", Password = "viewer123", Role = "Viewer" }
            );
        }
    }
}
