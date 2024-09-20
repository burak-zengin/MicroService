using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ordering.Api.Domain.Orders;
using Ordering.Api.Persistence.Configurations;

namespace Ordering.Api.Persistence;

public class OrderingDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public OrderingDbContext(IConfiguration configuration)
    {
        _configuration = configuration;

        Database.EnsureCreated();
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Sql"));

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new OrderConfiguration().Configure(modelBuilder.Entity<Order>());

        base.OnModelCreating(modelBuilder);
    }
}
