using BloodDonationApp.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Staff")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("UsersForEmployeeManagement/{hospitalId}")]
    public async Task<IActionResult> GetUsersForEmployeeManagement(Guid hospitalId)
    {
        var response = await _userService.GetUsersForEmployeeManagementAsync(hospitalId);
        return Ok(response);
    }
}
