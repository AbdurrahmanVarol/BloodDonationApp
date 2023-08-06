using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodDonationApp.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RequestsController : ControllerBase
{
    private readonly IRequestService _requestService;

    private Guid UserId => Guid.Parse(HttpContext.User.Claims.First(p => p.Type.Equals(ClaimTypes.NameIdentifier)).Value);

    public RequestsController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var requests = await _requestService.GetRequestsAsync();
        return Ok(requests);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var requests = await _requestService.GetByIdAsync(id);
        return Ok(requests);
    }

    [HttpGet("BloodGroup/{bloodGroupId}")]
    public async Task<IActionResult> GetRequestByBloodGroup(int bloodGroupId)
    {
        var requests = await _requestService.GetRequestsByBloodGroupIdAsync(bloodGroupId);
        return Ok(requests);
    }

    [HttpGet("User")]
    public async Task<IActionResult> GetRequestByUser()
    {
        var requests = await _requestService.GetRequestsByUserIdAsync(UserId);
        return Ok(requests);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> Post([FromBody] CreateRequestRequest request)
    {
        request.UserId = UserId;
        var id = await _requestService.AddAsync(request);
        return Created("", id);
    }

    [HttpPut]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> Put([FromBody] UpdateRequestRequest request)
    {
        await _requestService.UpdateAsync(request);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _requestService.DeleteAsync(id);
        return NoContent();
    }
}
