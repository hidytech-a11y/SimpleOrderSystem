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

    private readonly List<OrderStatusAudit> _statusAudits = new();
    public IReadOnlyCollection<OrderStatusAudit> StatusAudits => _statusAudits;

    public OrderStatus Status { get; private set; }

    private Order() { } // EF Core

    public Order(string orderNumber, string userId)
    {
        Id = Guid.NewGuid();
        OrderNumber = orderNumber;
        UserId = userId;
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    /* ===============================
     * STATUS UPDATE (WITH AUDIT)
     * =============================== */
    public void UpdateStatus(OrderStatus newStatus, string changedBy)
    {
        if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException(
                "Completed or cancelled orders cannot be modified.");

        if (Status == newStatus)
            return;

        var oldStatus = Status;
        Status = newStatus;

        _statusAudits.Add(new OrderStatusAudit(
            Id,
            changedBy,
            oldStatus,
            newStatus));
    }

    /* ===============================
     * ORDER ITEMS
     * =============================== */
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
