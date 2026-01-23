using SimpleOrderSystem.Application.DTOs;
using SimpleOrderSystem.Application.Interfaces;
using SimpleOrderSystem.Domain.Enums;
using SimpleOrderSystem.Domain.Interfaces;

namespace SimpleOrderSystem.Application.Services;

public class AdminDashboardService : IAdminDashboardService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public AdminDashboardService(
        IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<AdminDashboardDto> GetDashboardDataAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        var products = await _productRepository.GetAllAsync();

        return new AdminDashboardDto
        {
            TotalOrders = orders.Count(),
            PendingOrders = orders.Count(o => o.Status == OrderStatus.Pending),
            TotalRevenue = orders
                .Where(o => o.Status == OrderStatus.Completed)
                .Sum(o => o.TotalAmount),
            TotalProducts = products.Count()
        };
    }
}
