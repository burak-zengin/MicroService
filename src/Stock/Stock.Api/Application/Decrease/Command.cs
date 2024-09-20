namespace Stock.Api.Application.Decrease;

public record Command(int OrderId, List<Line> Lines);