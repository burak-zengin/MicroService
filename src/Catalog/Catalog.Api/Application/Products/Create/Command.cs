namespace Catalog.Api.Application.Products.Create;

public record Command(string Barcode, string Name, decimal UnitPrice);