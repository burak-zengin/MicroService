using Identity.Api.Domain.Roles;
using Identity.Api.Domain.Users;
using Identity.Api.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Persistence;

public class IdentityDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public IdentityDbContext(IConfiguration configuration)
    {
        _configuration = configuration;

        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Sql"));

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserConfiguration().Configure(modelBuilder.Entity<User>());
        new RoleConfiguration().Configure(modelBuilder.Entity<Role>());

        base.OnModelCreating(modelBuilder);
    }
}
