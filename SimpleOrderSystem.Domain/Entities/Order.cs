using SimpleOrderSystem.Domain.Enums;

namespace SimpleOrderSystem.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public string OrderNumber { get; private set; } = string.Empty;
    public string UserId { get; private set; } = string.Empty;
    public DateTime OrderDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    private readonly List<OrderStatusAudit> _statusAudits = new();
    public IReadOnlyCollection<OrderStatusAudit> StatusAudits => _statusAudits;

    private Order() { }

    public Order(string orderNumber, string userId)
    {
        Id = Guid.NewGuid();
        OrderNumber = orderNumber;
        UserId = userId;
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    public void AddItem(Guid productId, int quantity, decimal unitPrice)
    {
        _orderItems.Add(new OrderItem(productId, quantity, unitPrice));
        RecalculateTotal();
    }

    public void UpdateStatus(OrderStatus newStatus, string changedBy)
    {
        if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Finalized orders cannot be changed.");

        var oldStatus = Status;
        Status = newStatus;

        _statusAudits.Add(new OrderStatusAudit(
            Id,
            changedBy,
            oldStatus,
            newStatus));
    }

    private void RecalculateTotal()
    {
        TotalAmount = _orderItems.Sum(i => i.GetTotal());
    }
}
