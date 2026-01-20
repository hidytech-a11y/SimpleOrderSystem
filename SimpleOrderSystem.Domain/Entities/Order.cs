using SimpleOrderSystem.Domain.Enums;

namespace SimpleOrderSystem.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public string OrderNumber { get; private set; } = string.Empty;
    public string UserId { get; private set; } = string.Empty;
    public DateTime OrderDate { get; private set; }
    public decimal TotalAmount { get; private set; }

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public OrderStatus Status { get; private set; }


    private Order() { }

    public Order(string orderNumber, string userId)
    {
        Id = Guid.NewGuid();
        OrderNumber = orderNumber;
        UserId = userId;
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }


    public void UpdateStatus(OrderStatus status)
    {
        Status = status; 
    }

    public void AddItem(Guid productId, int quantity, decimal unitPrice)
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
