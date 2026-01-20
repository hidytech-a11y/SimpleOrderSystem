
using SimpleOrderSystem.Domain.Entities;
using SimpleOrderSystem.Domain.Enums;


namespace SimpleOrderSystem.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string userId, Dictionary<Guid, int> productQuantities);
        Task<IEnumerable<Order>> GetOrdersForUserAsync(string userId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task UpdateOrderStatusAsync(Guid orderId, OrderStatus status);
    }
}
