
using SimpleOrderSystem.Domain.Entities;


namespace SimpleOrderSystem.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string userId, Dictionary<int, int> productQuantities);
        Task<IEnumerable<Order>> GetOrdersForUserAsync(string userId);
    }
}
