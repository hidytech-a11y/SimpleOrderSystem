using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderSystem.Application.Interfaces;
using SimpleOrderSystem.Domain.Enums;

namespace SimpleOrderSystem.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IOrderService _orderService;

    public AdminController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return View(orders);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(Guid orderId, OrderStatus status)
    {
        await _orderService.UpdateOrderStatusAsync(orderId, status);
        return RedirectToAction(nameof(Index));
    }

}
