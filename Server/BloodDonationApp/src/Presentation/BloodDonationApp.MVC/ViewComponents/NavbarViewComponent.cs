using BloodDonationApp.Entities.Enums;
using BloodDonationApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodDonationApp.MVC.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userName = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;
            var result = Enum.TryParse(HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value, out Roles userRole);
            var isAuthenticated = HttpContext.User.Identity?.IsAuthenticated ?? false;

            return View(new NavbarViewModel
            {
                IsAuthenticated = isAuthenticated,
                UserName = userName,
                UserRole = result ? userRole : Roles.Donor
            });
        }
    }
}
