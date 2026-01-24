using SimpleOrderSystem.Application.Interfaces;
using SimpleOrderSystem.Domain.Entities;
using SimpleOrderSystem.Domain.Enums;
using SimpleOrderSystem.Domain.Interfaces;
using SimpleOrderSystem.Application.DTOs;

namespace SimpleOrderSystem.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderNumberGenerator _orderNumberGenerator;
    private readonly IOrderStatusAuditRepository _auditRepository;
    

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IOrderNumberGenerator orderNumberGenerator,
        IOrderStatusAuditRepository auditRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _orderNumberGenerator = orderNumberGenerator;
        _auditRepository = auditRepository;
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


    public async Task UpdateOrderStatusAsync(
        Guid orderId,
        OrderStatus status,
        string changedBy,
        byte[] rowVersion)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order == null)
            throw new InvalidOperationException("Order not found.");

        // 🔐 Optimistic Concurrency
        // Set the RowVersion property directly for concurrency check
        if (!order.RowVersion.SequenceEqual(rowVersion))
            throw new InvalidOperationException("The order was modified by another process.");

        order.UpdateStatus(status, changedBy);

        await _orderRepository.SaveChangesAsync();
    }



    public async Task<PagedResult<Order>> GetAdminOrdersAsync(AdminOrderQueryDto query)
    {
        var orders = (await _orderRepository.GetAllAsync()).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            orders = orders.Where(o =>
                o.OrderNumber.Contains(query.SearchTerm) ||
                o.UserId.Contains(query.SearchTerm));
        }

        if (query.Status.HasValue)
        {
            orders = orders.Where(o => o.Status == query.Status.Value);
        }

        var totalCount = orders.Count();

        var items = orders
            .OrderByDescending(o => o.OrderDate)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return new PagedResult<Order>
        {
            Items = items,
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }


}
