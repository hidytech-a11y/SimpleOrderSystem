namespace SimpleOrderSystem.Domain.Entities;

public class Order
{
    public int Id { get; private set; }
    public string OrderNumber { get; private set; } = string.Empty;
    public string UserId { get; private set; } = string.Empty;
    public DateTime OrderDate { get; private set; }
    public decimal TotalAmount { get; private set; }

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    private Order() { }

    public Order(string orderNumber, string userId)
    {
        OrderNumber = orderNumber;
        UserId = userId;
        OrderDate = DateTime.UtcNow;
    }

    public void AddItem(int productId, int quantity, decimal unitPrice)
    {
        var item = new OrderItem(productId, quantity, unitPrice);
        _orderItems.Add(item);
        RecalculateTotal();
    }

    private void RecalculateTotal()
    {
        TotalAmount = _orderItems.Sum(i => i.GetTotal());
    }
}
