using MassTransit;
using Shared.Events.Payment;

namespace Stock.Api.Consumers;

public class PaymentCompletedConsumer(Application.Decrease.Handler handler) : IConsumer<PaymentCompletedEvent>
{
    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        await handler.HandleAsync(new Application.Decrease.Command(context.Message.OrderId, context.Message.Lines.Select(l => new Application.Decrease.Line(l.Barcode, l.Quantity)).ToList()));
    }
}
