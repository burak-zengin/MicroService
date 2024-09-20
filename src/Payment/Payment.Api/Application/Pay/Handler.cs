using MassTransit;
using Shared.Events.Payment;

namespace Payment.Api.Application.Pay;

public class Handler(ISendEndpointProvider sendEndpointProvider)
{
    public async void Handle(Command command)
    {
        var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:PaymentCompleted"));
        await sendEndpoint.Send(new PaymentCompletedEvent(command.OrderId, command.Lines.Select(l => new Shared.Events.Line(l.Barcode, l.Quantity)).ToList()));
    }
}
