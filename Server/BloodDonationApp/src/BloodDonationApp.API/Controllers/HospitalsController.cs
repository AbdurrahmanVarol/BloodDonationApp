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

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var hospitals = await _hospitalService.GetByIdAsync(id);
        return Ok(hospitals);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post(CreateHospitalRequest createHospitalRequest)
    {
        var id = await _hospitalService.AddAsync(createHospitalRequest);
        return Created("", new { id });
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post(UpdateHospitalRequest updateHospitalRequest)
    {
        await _hospitalService.UpdateAsync(updateHospitalRequest);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _hospitalService.DeleteAsync(id);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeRequest addEmployeeRequest)
    {
        await _hospitalService.AddEmployeeAsync(addEmployeeRequest);
        return Ok(new { IsSuccess = true });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("[action]")]
    public async Task<IActionResult> RemoveEmployee([FromBody] RemoveEmployeeRequest removeEmployeeRequest)
    {
        await _hospitalService.RemoveEmployeeAsync(removeEmployeeRequest);
        return Ok(new { IsSuccess = true });

    }
}
