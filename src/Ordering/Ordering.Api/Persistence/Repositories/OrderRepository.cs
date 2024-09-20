using Microsoft.EntityFrameworkCore;
using Ordering.Api.Domain.Orders;

namespace Ordering.Api.Persistence.Repositories;

public class OrderRepository(OrderingDbContext context) : IOrderRepository
{
    public async Task CreateAsync(Order order)
    {
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
    }

    public async Task<int> ApproveAsync(int id)
    {
        return await context.Orders.Where(o => o.Id == id).ExecuteUpdateAsync(spc => spc.SetProperty(pe => pe.Status, OrderStatus.Approved));
    }
}
