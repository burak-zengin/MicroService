using Catalog.Api.Domain.Products;
using Catalog.Api.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Persistence;

public class CatalogDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public CatalogDbContext(IConfiguration configuration)
    {
        _configuration = configuration;

        Database.EnsureCreated();
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Sql"));

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ProductConfiguration().Configure(modelBuilder.Entity<Product>());

        base.OnModelCreating(modelBuilder);
    }
}
