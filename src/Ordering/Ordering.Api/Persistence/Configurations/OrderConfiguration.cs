using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Api.Domain.Orders;

namespace Ordering.Api.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(o => o.Customer);
        builder.Property(o => o.TotalAmount).UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
    }
}
