using System;
using System.Collections.Generic;
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

            var theData = new List<Product>
            {
                new Product(Guid.NewGuid(), "Barby", "This is a barby.", 1, "Mattel", 123),
                new Product(Guid.NewGuid(), "Ken", "This is a Ken.", 1, "Mattel", 123),
                new Product(Guid.NewGuid(), "Winnie the poo", "This is a barby.", 1, "Mattel", 123),
                new Product(Guid.NewGuid(), "Goku", "This is a barby.", 1, "Mattel", 123),
                new Product(Guid.NewGuid(), "Vegeta", "This is a barby.", 1, "Mattel", 123),
                new Product(Guid.NewGuid(), "King kong", "This is a barby.", 1, "Mattel", 123),
                new Product(Guid.NewGuid(), "Smurfs set", "This is a barby.", 1, "Mattel", 123),
                new Product(Guid.NewGuid(), "Lord of the ring", "This is a barby.", 1, "Mattel", 123),
                new Product(Guid.NewGuid(), "Barby II", "This is a barby.", 1, "Mattel", 123),
                new Product(Guid.NewGuid(), "Barby III", "This is a barby.", 1, "Mattel", 123),
                new Product(Guid.NewGuid(), "Barby IV", "This is a barby.", 1, "Mattel", 123)
            };

            for (int i = 0; i <= theData.Count - 1; i++)
            {
                var theCurrentOne = theData[i];
                theCurrentOne.Id = i + 1;
            }

            modelBuilder.Entity<Product>()
                .HasData(theData);


            base.OnModelCreating(modelBuilder);
        }
    }
}