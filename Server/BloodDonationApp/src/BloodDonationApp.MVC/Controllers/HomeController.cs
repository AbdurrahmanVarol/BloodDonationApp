using BloodDonationApp.Business.Services;
using BloodDonationApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BloodDonationApp.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRequestService _requestService;

    public HomeController(ILogger<HomeController> logger, IRequestService requestService)
    {
        _logger = logger;
        _requestService = requestService;
    }

    public async Task<IActionResult> Index()
    {
        var isAuthenticated = HttpContext.User.Identity?.IsAuthenticated ?? false;
        ViewBag.IsAuthenticated = isAuthenticated;
        if (isAuthenticated)
        {
            var userName = HttpContext.User.Claims.First(p => p.Type.Equals(ClaimTypes.Name)).Value;
            ViewBag.UserName = userName;

            var bloodGroupId = int.Parse(HttpContext.User.Claims.First(p => p.Type.Equals("BloodGroup")).Value);
            var requestsByBloodGroup = await _requestService.GetRequestsByBloodGroupId(bloodGroupId);
            return View(requestsByBloodGroup);

        }
        var requests = await _requestService.GetRequests();
        return View(requests);
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
    [HttpGet]
    public IActionResult NotFoundPage()
    {
        return View();
    }
}