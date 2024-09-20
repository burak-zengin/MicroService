using Ordering.Api.Domain.Orders;
using Shared.Results;

namespace Ordering.Api.Application.Orders.Approved;

public class Handler(IOrderRepository repository)
{
    public async Task<Result> HandleAsync(Command command)
    {
        var approvedOrderCount = await repository.ApproveAsync(command.Id);
        if (approvedOrderCount == 0)
        {
            return new Result
            {
                Failed = true,
                Messages = ["Order not approved."]
            };
        }

        return new Result();
    }
}
