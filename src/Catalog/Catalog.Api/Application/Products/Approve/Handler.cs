using Catalog.Api.Domain.Products;
using MassTransit;
using Shared.Results;

namespace Catalog.Api.Application.Products.Approve;

public class Handler(IProductRepository repository, IPublishEndpoint publishEndpoint)
{
    public async Task<Result> HandleAsync(Command command)
    {
        var approvedProductCount = await repository.ApproveAsync(command.Barcode);
        if (approvedProductCount == 0)
        {
            return new Result
            {
                Failed = true,
                Messages = ["Product not approved."]
            };
        }

        return new Result();
    }
}
