namespace Ordering.Api.Domain.Orders;

public interface IOrderRepository
{
    Task CreateAsync(Order order);

    Task<int> ApproveAsync(int id);
}
