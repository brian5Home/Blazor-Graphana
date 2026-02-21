using BlazorGrafanaApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorGrafanaApp.Core.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).HasMaxLength(200).IsRequired();
            e.Property(p => p.Description).HasMaxLength(1000);
            e.Property(p => p.Category).HasMaxLength(100).IsRequired();
            e.Property(p => p.UnitPrice).HasPrecision(18, 2);
        });
    }
}
