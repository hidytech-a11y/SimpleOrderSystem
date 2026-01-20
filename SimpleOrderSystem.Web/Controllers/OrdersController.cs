using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderSystem.Application.Interfaces;
using SimpleOrderSystem.Domain.Interfaces;
using SimpleOrderSystem.Web.Models;

namespace SimpleOrderSystem.Web.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IProductRepository _productRepository;

    public OrdersController(
        IOrderService orderService,
        IProductRepository productRepository)
    {
        _orderService = orderService;
        _productRepository = productRepository;
    }

    
    // MY ORDERS
    
    public async Task<IActionResult> Index()
    {
        var userId = User.Identity!.Name!;
        var orders = await _orderService.GetOrdersForUserAsync(userId);
        return View(orders);
    }

    
    // CREATE ORDER (GET)
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var products = await _productRepository.GetAllAsync();

        var model = new CreateOrderViewModel
        {
            Products = products.Select(p => new ProductOrderItem
            {
                ProductId = p.Id,          //  Guid
                ProductName = p.Name,
                Price = p.Price,
                Quantity = 0
            }).ToList()
        };

        return View(model);
    }

    
    // CREATE ORDER (POST)
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateOrderViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Build Dictionary<Guid, int>
        var selectedProducts = model.Products
            .Where(p => p.Quantity > 0)
            .ToDictionary(
                p => p.ProductId,   //  Guid
                p => p.Quantity);

        if (!selectedProducts.Any())
        {
            ModelState.AddModelError("", "Please select at least one product.");
            return View(model);
        }

        var userId = User.Identity!.Name!;

        var order = await _orderService.CreateOrderAsync(
            userId,
            selectedProducts);

        return RedirectToAction(
            nameof(Success),
            new { orderNumber = order.OrderNumber });
    }

    
    // ORDER SUCCESS
    
    [HttpGet]
    public IActionResult Success(string orderNumber)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            return RedirectToAction(nameof(Index));

        ViewBag.OrderNumber = orderNumber;
        return View();
    }
}
