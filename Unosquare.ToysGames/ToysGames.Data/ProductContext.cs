using Microsoft.EntityFrameworkCore;
using ToysGames.Data.Models;

namespace ToysGames.Data
{
    /// <summary>
    /// This class has all the required code to manage the data storage of this system.
    /// </summary>
    public class ProductContext : DbContext
    {
        public ProductContext()
        {
            
        }

        public ProductContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(b =>
            {
                b.Property("_id");
                b.HasKey("_id");
                b.Property(e => e.ProductId);
                b.Property(e => e.Name);
                b.Property(e => e.Description);
                b.Property(e => e.AgeRestriction);
                b.Property(e => e.Company);
                b.Property(e => e.Price);
            });

            modelBuilder.Entity<Product>()
                .HasIndex(b => b.ProductId)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}