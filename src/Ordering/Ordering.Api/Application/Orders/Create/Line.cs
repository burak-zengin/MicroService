namespace Ordering.Api.Application.Orders.Create;

public record Line(int Quantity, string Barcode, string ProductName, decimal UnitPrice);
