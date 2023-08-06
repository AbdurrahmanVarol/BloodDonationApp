using BloodDonationApp.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GendersController : ControllerBase
{
    private readonly IGenderService _genderService;

    public GendersController(IGenderService genderService)
    {
        _genderService = genderService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var genders = await _genderService.GetGendersAsync();
        return Ok(genders);
    }
}
