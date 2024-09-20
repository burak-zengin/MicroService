using Catalog.Api.Domain.Products;
using MassTransit;
using Shared.Events.Catalog;
using Shared.Results;

namespace Catalog.Api.Application.Products.Create;

public class Handler(IProductRepository repository, ISendEndpointProvider sendEndpointProvider)
{
    public async Task<Result<int>> HandleAsync(Command command)
    {
        if (await repository.AnyAsync(command.Barcode))
        {
            return new Result<int>
            {
                Failed = true,
                Messages = ["Barcode is used."]
            };
        }

        var product = new Product
        {
            Barcode = command.Barcode,
            Name = command.Name,
            UnitPrice = command.UnitPrice,
            Status = ProductStatus.Waiting
        };

        await repository.CreateAsync(product);

        var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:ProductCreated"));
        await sendEndpoint.Send(new ProductCreatedEvent(product.Barcode));

        return new Result<int>
        {
            Data = product.Id
        };
    }
}
