using MassTransit;
using Shared.Events.Stock;

namespace Catalog.Api.Consumers;

public class StockCreatedConsumer(Application.Products.Approve.Handler handler) : IConsumer<StockCreatedEvent>
{
    public async Task Consume(ConsumeContext<StockCreatedEvent> context)
    {
        await handler.HandleAsync(new Application.Products.Approve.Command(context.Message.Barcode));
    }
}
