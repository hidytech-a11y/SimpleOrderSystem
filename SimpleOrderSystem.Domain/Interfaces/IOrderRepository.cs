using SimpleOrderSystem.Domain.Entities;

namespace SimpleOrderSystem.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<IEnumerable<Order>> GetByUserIdAsync(string userId);
    }
}
