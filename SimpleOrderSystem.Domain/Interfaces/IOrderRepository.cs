using SimpleOrderSystem.Domain.Entities;

namespace SimpleOrderSystem.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<IEnumerable<Order>> GetByUserIdAsync(string userId);

        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order> GetByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}
