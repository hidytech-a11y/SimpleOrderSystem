using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderSystem.Application.Interfaces;

namespace SimpleOrderSystem.Web.Controllers;



[Authorize]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.Identity!.Name!;
        var orders = await _orderService.GetOrdersForUserAsync(userId);
        return View(orders);

    }
}
