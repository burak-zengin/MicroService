namespace Payment.Api.Application.Pay;

public record Command(int OrderId, string Name, string Number, int Month, int Year, List<Line> Lines);