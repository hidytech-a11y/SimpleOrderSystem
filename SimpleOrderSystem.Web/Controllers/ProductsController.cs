using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderSystem.Application.Interfaces;
using SimpleOrderSystem.Domain.Entities;

namespace SimpleOrderSystem.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // ============================
    // PUBLIC — VIEW PRODUCTS
    // ============================
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();
        return View(products);
    }

    // ============================
    // ADMIN — CREATE PRODUCT
    // ============================


    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name) || price <= 0)
        {
            ModelState.AddModelError("", "Invalid product details.");
            return View();
        }

        await _productService.CreateAsync(name, price);
        return RedirectToAction(nameof(Index));
    }

    // ============================
    // ADMIN — EDIT PRODUCT
    // ============================
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, string name, decimal price)
    {
        await _productService.UpdateAsync(id, name, price);
        return RedirectToAction(nameof(Index));
    }

    // ============================
    // ADMIN — DELETE PRODUCT
    // ============================
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
