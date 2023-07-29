using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Services;
using BloodDonationApp.Entities.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodDonationApp.MVC.Controllers;

public class AuthController : Controller
{
    private readonly IMvcAuthService _authService;

    public AuthController(IMvcAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest loginRequest, string? returnUrl)
    {
        try
        {
            var user = await _authService.LoginAsync(loginRequest);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim("BloodGroup",user.BloodGroupId.ToString()),
                new Claim(ClaimTypes.Role,((Roles)user.RoleId).ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authenticationProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = loginRequest.IsKeepLoggedIn,
            };

            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authenticationProperties);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("index", "home");
            }
            return Redirect(returnUrl);
        }
        catch
        {
            TempData["LoginException"] = "Kullanıcı adı ya da şifre hatalı";
            return View();
        }
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("login");
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {

        try
        {
            await _authService.RegisterAsync(registerRequest);
            TempData["Register"] = "Kayıt olundu";
            return View();
        }
        catch
        {
            TempData["Register"] = "Kayıt olunmadı";
            return View();
        }
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
