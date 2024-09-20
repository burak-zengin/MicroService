using Catalog.Api.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Persistence.Repositories;

public class ProductRepository(CatalogDbContext context) : IProductRepository
{
    public async Task<int> ApproveAsync(string barcode)
    {
        return await context.Products.Where(p => p.Barcode == barcode).ExecuteUpdateAsync(p => p.SetProperty(p => p.Status, ProductStatus.Approved));
    }

    public async Task<bool> AnyAsync(string barcode)
    {
        return await context.Products.AnyAsync(p => p.Barcode == barcode);
    }

    public async Task CreateAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
    }
}
