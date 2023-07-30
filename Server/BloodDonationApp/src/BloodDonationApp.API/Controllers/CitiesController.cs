using BloodDonationApp.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly ICityService _cityService;

    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var cities = await _cityService.GetCitiesAsync();
        return Ok(cities);
    }
}
