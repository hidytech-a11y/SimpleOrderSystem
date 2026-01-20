using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderSystem.Application.DTOs;
using SimpleOrderSystem.Application.Interfaces;

namespace SimpleOrderSystem.Web.Controllers.Api;



[ApiController]
[Route("api/orders")]
[Authorize]

public class OrdersApiController : ControllerBase
{
   private readonly IOrderService _orderService;

    public OrdersApiController(IOrderService orderService)
    {
         _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Dictionary<Guid, int> products)
    {
        var userId = User.Identity!.Name!;
        var order = await _orderService.CreateOrderAsync(userId, products);
        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = User.Identity!.Name!;
        var orders = await _orderService.GetOrdersForUserAsync(userId);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.Identity!.Name!;
        var order = await _orderService.CreateOrderAsync(userId, dto.ProductQuantities);
        return Ok(order);
    }

}

