using MassTransit;
using Shared.Events.Stock;
using Shared.Results;
using Stock.Api.Domain.Stocks;

namespace Stock.Api.Application.Create;

public class Handler(IStockRepository repository, ISendEndpointProvider sendEndpointProvider)
{
    public async Task<Result> HandleAsync(Command command)
    {
        repository.CreateAsync(new Domain.Stocks.Stock
        {
            Barcode = command.Barcode,
            Quantity = command.Quantity
        });

        var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:StockCreated"));
        await sendEndpoint.Send(new StockCreatedEvent
        {
            Barcode = command.Barcode
        });

        return new Result();
    }
}
