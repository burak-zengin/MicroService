namespace Shared.Events.Payment;

public record PaymentCompletedEvent(int OrderId, List<Line> Lines);