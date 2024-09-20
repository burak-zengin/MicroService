namespace Catalog.Api.Domain.Products;

public class Product
{
    public int Id { get; set; }

    public string Barcode { get; set; }

    public string Name { get; set; }

    public decimal UnitPrice { get; set; }

    public ProductStatus Status { get; set; }
}
