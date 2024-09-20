using MassTransit;
using Shared.Events.Stock;
using Shared.Results;
using Stock.Api.Domain.Stocks;

namespace Stock.Api.Application.Decrease;

public class Handler(IStockRepository repository, ISendEndpointProvider sendEndpointProvider)
{
    public async Task<Result> HandleAsync(Command command)
    {
        repository.DecreaseAsync(command.Lines.Select(l => new Domain.Stocks.Stock
        {
            Barcode = l.Barcode,
            Quantity = l.Quantity
        }).ToList());

        var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:StockDecreased"));
        await sendEndpoint.Send(new StockDecreasedEvent(command.OrderId));

        return new Result();
    }
}
