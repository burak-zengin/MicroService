using MassTransit;
using Shared.Events.Ordering;

namespace Payment.Api.Consumers;

public class OrderCreatedConsumer(Application.Pay.Handler handler) : IConsumer<OrderCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        handler.Handle(new Application.Pay.Command(
            context.Message.Id,
            context.Message.CreditCard.Name,
            context.Message.CreditCard.Number,
            context.Message.CreditCard.Month,
            context.Message.CreditCard.Year,
            context.Message.Lines.Select(l => new Application.Pay.Line(l.Barcode, l.Quantity)).ToList()));

        return Task.CompletedTask;
    }
}
