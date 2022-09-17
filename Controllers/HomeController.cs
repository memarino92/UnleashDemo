using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UnleashDemo.Models;
using Unleash;
using System.Collections.Generic;

namespace UnleashDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnleash _unleash;

    public HomeController(ILogger<HomeController> logger, IUnleash unleash)
    {
        _logger = logger;

        _unleash = unleash;
    }

    public IActionResult Index()
    {
        return View(new IndexViewModel { DemoFeatureEnabled = _unleash.IsEnabled("demo-feature") });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
