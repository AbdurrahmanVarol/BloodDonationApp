using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.MVC.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> GetUsersForEmployeeManagement(Guid hospitalId)
    {
        EmployeeManagementResponse response = await _userService.GetUsersForEmployeeManagement(hospitalId);
        return Json(response);
    }
}
