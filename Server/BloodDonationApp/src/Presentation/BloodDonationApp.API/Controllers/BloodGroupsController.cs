using BloodDonationApp.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BloodGroupsController : ControllerBase
{
    private readonly IBloodGroupService _bloodGroupService;

    public BloodGroupsController(IBloodGroupService bloodGroupService)
    {
        _bloodGroupService = bloodGroupService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var bloodGroups = await _bloodGroupService.GetBloodGroupsAsync();
        return Ok(bloodGroups);
    }
}
