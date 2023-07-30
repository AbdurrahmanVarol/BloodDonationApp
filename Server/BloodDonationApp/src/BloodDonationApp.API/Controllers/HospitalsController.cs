using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class HospitalsController : ControllerBase
{
    private readonly IHospitalService _hospitalService;

    public HospitalsController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var hospitals = await _hospitalService.GetHospitalsAsync();
        return Ok(hospitals);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post(CreateHospitalRequest createHospitalRequest)
    {
        var id = await _hospitalService.AddAsync(createHospitalRequest);
        return Ok(new { id });
    }
}
