using SimpleOrderSystem.Domain.Enums;

namespace SimpleOrderSystem.Domain.Entities;

public class OrderStatusAudit
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public string ChangedBy { get; private set; } = string.Empty;
    public OrderStatus OldStatus { get; private set; }
    public OrderStatus NewStatus { get; private set; }
    public DateTime ChangedAt { get; private set; }

    private OrderStatusAudit() { }

    public OrderStatusAudit(
        Guid orderId,
        string changedBy,
        OrderStatus oldStatus,
        OrderStatus newStatus)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        ChangedBy = changedBy;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedAt = DateTime.UtcNow;
    }
}
