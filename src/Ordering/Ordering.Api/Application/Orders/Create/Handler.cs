using MassTransit;
using Ordering.Api.Domain.Orders;
using Shared.Events.Ordering;
using Shared.Results;

namespace Ordering.Api.Application.Orders.Create;

public class Handler(IOrderRepository repository, ISendEndpointProvider sendEndpointProvider)
{
    public async Task<Result<int>> HandleAsync(Command command)
    {
        var order = new Order
        {
            Customer = new Domain.Orders.Customer
            {
                Name = command.Customer.Name,
                Surname = command.Customer.Surname
            },
            Lines = command.Lines.Select(l => new Domain.Orders.Line
            {
                ProductName = l.ProductName,
                Quantity = l.Quantity,
                UnitPrice = l.UnitPrice
            }).ToList()
        };

        await repository.CreateAsync(order);

        var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:OrderCreated"));
        sendEndpoint.Send(new OrderCreatedEvent(
            order.Id,
            new Shared.Events.CreditCard(
                command.CreditCard.Name,
                command.CreditCard.Number,
                command.CreditCard.Month,
                command.CreditCard.Year),
            command
                .Lines
                .Select(l => new Shared.Events.Line(
                    l.Barcode,
                    l.Quantity))
                .ToList()));

        return new Result<int>
        {
            Data = order.Id
        };
    }
}
