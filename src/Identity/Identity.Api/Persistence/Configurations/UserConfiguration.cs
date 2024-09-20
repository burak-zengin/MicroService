using Identity.Api.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Api.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Username).IsUnique();

        builder.HasData(new User
        {
            Id = 1,
            Username = "OrderingClient",
            Password = "123456",
            RoleId = "O"
        }, new User
        {
            Id = 2,
            Username = "CatalogClient",
            Password = "123456",
            RoleId = "C"
        });
    }
}
