using MassTransit;
using Shared.Events.Stock;

namespace Ordering.Api.Consumers;

public class StockDecreasedConsumer(Application.Orders.Approved.Handler handler) : IConsumer<StockDecreasedEvent>
{
    public async Task Consume(ConsumeContext<StockDecreasedEvent> context)
    {
        await handler.HandleAsync(new Application.Orders.Approved.Command(context.Message.Id));
    }
}
