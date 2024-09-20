using MassTransit;
using Shared.Events.Catalog;

namespace Stock.Api.Consumers;

public class ProductCreatedConsumer(Application.Create.Handler handler) : IConsumer<ProductCreatedEvent>
{
    public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        await handler.HandleAsync(new Application.Create.Command(context.Message.Barcode, 0));
    }
}
