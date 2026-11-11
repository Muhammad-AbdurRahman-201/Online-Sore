using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
     : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>().Property(i => i.UnitPrice).HasPrecision(18, 2);

            // ek user ka ek product cart mein sirf ek hi row
        modelBuilder    .Entity<Cart>().HasIndex(c => new {c.UserId, c.ProductId }).IsUnique();
            modelBuilder.Entity<Product>().HasData(
       new Product
       {
           Id = 1,
           Title = "iPhone 17",
           Category = "Smartphones",
           Description = "Latest Apple iPhone with advanced performance and camera.",
           Price = 750m,
           StockQuantity = 20,
           CreatedAt = new DateTime(2026, 10, 1)
       },
       new Product
       {
           Id = 2,
           Title = "Samsung Galaxy S26",
           Category = "Smartphones",
           Description = "Premium Samsung smartphone with AMOLED display.",
           Price = 680m,
           StockQuantity = 15,
           CreatedAt = new DateTime(2026, 10, 1)
       },
       new Product
       {
           Id = 3,
           Title = "Dell Inspiron 15",
           Category = "Laptops",
           Description = "Powerful laptop suitable for work, study and everyday use.",
           Price = 850m,
           StockQuantity = 10,
           CreatedAt = new DateTime(2026, 10, 1)
       },
       new Product
       {
           Id = 4,
           Title = "HP Pavilion 14",
           Category = "Laptops",
           Description = "Slim and lightweight laptop with excellent performance.",
           Price = 780m,
           StockQuantity = 12,
           CreatedAt = new DateTime(2026, 10, 1)
       },
       new Product
       {
           Id = 5,
           Title = "Sony WH-1000XM6",
           Category = "Headphones",
           Description = "Wireless noise-cancelling headphones with premium sound.",
           Price = 350m,
           StockQuantity = 25,
           CreatedAt = new DateTime(2026, 10, 1)
       },
       new Product
       {
           Id = 6,
           Title = "Apple AirPods Pro",
           Category = "Headphones",
           Description = "Wireless earbuds with active noise cancellation.",
           Price = 250m,
           StockQuantity = 30,
           CreatedAt = new DateTime(2026, 10, 1)
       },
       new Product
       {
           Id = 7,
           Title = "Logitech MX Master 3S",
           Category = "Accessories",
           Description = "Ergonomic wireless mouse designed for productivity.",
           Price = 95m,
           StockQuantity = 40,
           CreatedAt = new DateTime(2026, 10, 1)
       },
       new Product
       {
           Id = 8,
           Title = "Keychron K2",
           Category = "Keyboards",
           Description = "Compact mechanical wireless keyboard for work and gaming.",
           Price = 110m,
           StockQuantity = 18,
           CreatedAt = new DateTime(2026, 10, 1)
       },
       new Product
       {
           Id = 9,
           Title = "Anker Power Bank 20,000mAh",
           Category = "Accessories",
           Description = "High-capacity portable power bank with fast charging.",
           Price = 60m,
           StockQuantity = 50,
           CreatedAt = new DateTime(2026, 10, 1)
       },
       new Product
       {
           Id = 10,
           Title = "Samsung 27-inch Monitor",
           Category = "Monitors",
           Description = "27-inch Full HD monitor suitable for work and entertainment.",
           Price = 220m,
           StockQuantity = 22,
           CreatedAt = new DateTime(2026, 10, 1)
       }
   );



        }
    }
}