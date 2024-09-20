namespace Ordering.Api.Domain.Orders;

public class Order
{
    public int Id { get; set; }

    public OrderStatus Status { get; set; }

    public Customer Customer { get; set; }

    private decimal _totalAmount;
    public decimal TotalAmount
    {
        get => Lines.Sum(l => l.Quantity * l.UnitPrice);
        private set => _totalAmount = value;
    }

    public List<Line> Lines { get; set; }
}
