using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderSystem.Application.DTOs;
using SimpleOrderSystem.Application.Interfaces;
using SimpleOrderSystem.Application.Services;
using SimpleOrderSystem.Domain.Enums;
using System.Security.Claims;

namespace SimpleOrderSystem.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IAdminDashboardService _dashboardService;

    public AdminController(
        IOrderService orderService,
        IAdminDashboardService dashboardService)
    {
        _orderService = orderService;
        _dashboardService = dashboardService;
    }

    
    // DASHBOARD
    
    public async Task<IActionResult> Index()
    {
        var dashboard = await _dashboardService.GetDashboardDataAsync();
        return View(dashboard);
    }

    
    // ALL ORDERS
    

    [HttpGet]
    public async Task<IActionResult> Orders(
        string? searchTerm,
        OrderStatus? status,
        int page = 1)
    {
        var query = new AdminOrderQueryDto
        {
            SearchTerm = searchTerm,
            Status = status,
            Page = page,
            PageSize = 10
        };

        var result = await _orderService.GetAdminOrdersAsync(query);

        ViewBag.SearchTerm = searchTerm;
        ViewBag.Status = status;

        return View(result);
    }


    // UPDATE STATUS (POST ONLY)

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateOrderStatus(Guid orderId, OrderStatus status)
    {
        // Ensure we provide a non-null changedBy to avoid inserting a null into
        // the non-nullable ChangedBy column (prevents DB update failures).
        var changedBy = User?.Identity?.Name;

        if (string.IsNullOrWhiteSpace(changedBy))
        {
            // Prefer email claim, then name identifier, then fallback to "System"
            changedBy = User?.FindFirst(ClaimTypes.Email)?.Value
                        ?? User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? "System";
        }

        await _orderService.UpdateOrderStatusAsync(
            orderId,
            status,
            changedBy);

        return RedirectToAction(nameof(Orders));
    }


}
