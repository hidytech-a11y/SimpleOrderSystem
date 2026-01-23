using Microsoft.EntityFrameworkCore;
using SimpleOrderSystem.Domain.Entities;
using SimpleOrderSystem.Domain.Interfaces;

namespace SimpleOrderSystem.Infrastructure.Data.Repositories;

public class OrderStatusAuditRepository : IOrderStatusAuditRepository
{
    private readonly ApplicationDbContext _context;

    public OrderStatusAuditRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(OrderStatusAudit audit)
    {
        await _context.OrderStatusAudits.AddAsync(audit);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderStatusAudit>> GetByOrderIdAsync(Guid orderId)
    {
        return await _context.OrderStatusAudits
            .Where(a => a.OrderId == orderId)
            .OrderByDescending(a => a.ChangedAt)
            .ToListAsync();
    }
}
