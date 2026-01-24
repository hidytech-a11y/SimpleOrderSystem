
using SimpleOrderSystem.Domain.Entities;
using SimpleOrderSystem.Domain.Enums;
using SimpleOrderSystem.Application.DTOs;


namespace SimpleOrderSystem.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string userId, Dictionary<Guid, int> productQuantities);
        Task<IEnumerable<Order>> GetOrdersForUserAsync(string userId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();


        //Extend Order Service Contract
        Task<PagedResult<Order>> GetAdminOrdersAsync(AdminOrderQueryDto query);

        Task UpdateOrderStatusAsync(
            Guid orderId,
            OrderStatus status,
            string changedBy,
            byte[] rowVersion);

    }
}
