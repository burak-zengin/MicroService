namespace Catalog.Api.Domain.Products;

public interface IProductRepository
{
    Task CreateAsync(Product product);

    Task<bool> AnyAsync(string barcode);

    Task<int> ApproveAsync(string barcode);
}
