using Microsoft.EntityFrameworkCore;
using MiniOrderApi.Entities;

namespace MiniOrderApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Customer)      
                .WithOne(c => c.User)         
                .HasForeignKey<Customer>(c => c.UserId); 

            base.OnModelCreating(modelBuilder);
        }
    }
}