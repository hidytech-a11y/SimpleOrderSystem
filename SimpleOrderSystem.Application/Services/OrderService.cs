using SimpleOrderSystem.Application.Interfaces;
using SimpleOrderSystem.Domain.Entities;
using SimpleOrderSystem.Domain.Enums;
using SimpleOrderSystem.Domain.Interfaces;

namespace SimpleOrderSystem.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderNumberGenerator _orderNumberGenerator;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IOrderNumberGenerator orderNumberGenerator)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _orderNumberGenerator = orderNumberGenerator;
    }

    public async Task<Order> CreateOrderAsync(string userId, Dictionary<Guid, int> productQuantities)
    {
        if (!productQuantities.Any())
            throw new ArgumentException("Order must contain at least one product.");

        var orderNumber = _orderNumberGenerator.Generate();
        var order = new Order(orderNumber, userId);

        foreach (var item in productQuantities)
        {
            var product = await _productRepository.GetByIdAsync(item.Key);

            if (product is null)
                throw new InvalidOperationException($"Product {item.Key} not found.");

            order.AddItem(
                product.Id,
                item.Value,
                product.Price);
        }

        await _orderRepository.AddAsync(order);

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersForUserAsync(string userId)
    {
        return await _orderRepository.GetByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    public async Task UpdateOrderStatusAsync(Guid orderId, OrderStatus status)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order == null)
            throw new InvalidOperationException("Order not found");

        order.UpdateStatus(status);

        await _orderRepository.SaveChangesAsync();
    }
}
