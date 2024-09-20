namespace Ordering.Api.Application.Orders.Create;

public record Command(Customer Customer, List<Line> Lines, CreditCard CreditCard);