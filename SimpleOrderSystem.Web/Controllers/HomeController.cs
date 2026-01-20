using Microsoft.AspNetCore.Mvc;
using SimpleOrderSystem.Web.Models;
using System.Diagnostics;

namespace SimpleOrderSystem.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
