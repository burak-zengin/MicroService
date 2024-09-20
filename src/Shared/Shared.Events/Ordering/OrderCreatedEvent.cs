namespace Shared.Events.Ordering;

public record OrderCreatedEvent(int Id, CreditCard CreditCard, List<Line> Lines);