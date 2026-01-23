using SimpleOrderSystem.Domain.Entities;

namespace SimpleOrderSystem.Domain.Interfaces;

public interface IOrderStatusAuditRepository
{
    Task AddAsync(OrderStatusAudit audit);
    Task<IEnumerable<OrderStatusAudit>> GetByOrderIdAsync(Guid orderId);
}
