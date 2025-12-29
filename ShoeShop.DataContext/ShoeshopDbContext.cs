using System;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Entities;

namespace ShoeShop.DataContext;

public class ShoeshopDbContext : DbContext
{
    public ShoeshopDbContext(DbContextOptions<ShoeshopDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.Description)
                .HasMaxLength(2000);

            entity.Property(p => p.ImageUrl)
                .HasMaxLength(500);

            entity.Property(p => p.Sku)
                .HasMaxLength(100);

            entity.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.SupplierPrice)
                .HasColumnType("decimal(18,2)");

            // One supplier -> many products (product requires a supplier)
            entity.HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Category
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);
        });

        // Supplier
        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(s => s.ContactName)
                .HasMaxLength(150);

            entity.Property(s => s.Email)
                .HasMaxLength(200);

            entity.Property(s => s.Phone)
                .HasMaxLength(50);

            entity.Property(s => s.Address)
                .HasMaxLength(500);
        });
        // One-to-many Supplier -> Products
        modelBuilder.Entity<Supplier>()
            .HasMany(s => s.Products)
            .WithOne(p => p.Supplier)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        // Many-to-many Product ↔ Category
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity(j => j.ToTable("ProductCategories"));
    }

}
