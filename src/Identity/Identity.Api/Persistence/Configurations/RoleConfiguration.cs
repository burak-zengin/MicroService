using Identity.Api.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Api.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasIndex(r => r.Name).IsUnique();

        builder.HasData(new Role
        {
            Id = "O",
            Name = "Ordering"
        }, new Role
        {
            Id = "C",
            Name = "Catalog"
        });
    }
}
